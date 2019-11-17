/* Master only */
/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */
/*
**  OLEDB_INSTALL
**  This file contains the metadata Stored Procedures used by the OLEDB drivers.
**
*/

/*
 ** Stored procedures created:
 **
 ** Name                          	Default Location	Comments
 ** --------------------------------------------------------------------------------------------------
 ** 1>  sp_oledb_server_info		sybsystemprocs
 ** 2>  sp_oledb_databases		sybsystemprocs
 ** 3>  sp_oledb_columns		sybsystemprocs
 ** 4>  sp_oledb_datatype_info		sybsystemprocs
 ** 5>  sp_oledb_getversioncolumns	sybsystemprocs		
 ** 6>  sp_oledb_getcolumnprivileges	sybsystemprocs
 ** 7>  sp_oledb_tables			sybsystemprocs
 ** 8>  sp_oledb_gettableprivileges	sybsystemprocs
 ** 9>  sp_oledb_getindexinfo		sybsystemprocs
 ** 10> sp_oledb_primarykey		sybsystemprocs
 ** 11> sp_oledb_fkeys			sybsystemprocs
 ** 12> sp_oledb_stored_procedures	sybsystemprocs
 ** 13> sp_oledb_getprocedurecolumns	sybsystemprocs
 ** 14> sp_oledb_computeprivs		sybsystemprocs	added to support 
 ** 15> sp_oledb_gettableprivileges
 ** 16> sp_oledb_getcolumnprivileges
 ** 17> sp_oledb_views
 ** 18> sp_version			sybsystemprocs
 */
 
set flushmessage on
go

print ""
go


-- select @version_string = "@oledbMajorVersion@.@oledbMinorVersion1@.@oledbMinorVersion2@.@BuildNumber@"
declare @version_string varchar(255) 
select @version_string = '15.0.0.325/'+ 'Thu 08-14-2008 14:58:04.98' 
print "OLEDB - Build Version : %1!", @version_string 
declare @retval int
exec @retval = sp_version 'OLEDB MDA Scripts', NULL, @version_string, 'start'
if (@retval != 0) select syb_quit()
go
use sybsystemprocs
go



if exists (select * from sysobjects where name = 'spt_sybdrv')
begin			
	drop table spt_sybdrv
end
go
print "Creating spt_sybdrv"
go

create table spt_sybdrv
(type_name varchar(20) Primary Key,
  data_type int, tds_type int null)
go
begin 
 insert into spt_sybdrv values("smallint",2,52)
 insert into spt_sybdrv values("int",3, 56)
 insert into spt_sybdrv values("real",4, 59)
 insert into spt_sybdrv values("float",5, 62)
 insert into spt_sybdrv values("money",6, 60)
 insert into spt_sybdrv values("smallmoney",6,122)
 insert into spt_sybdrv values("bit",11, 50)
 insert into spt_sybdrv values("tinyint",17,48)
 insert into spt_sybdrv values("binary",128,45)
 insert into spt_sybdrv values("image",128,34)
 insert into spt_sybdrv values("varbinary",128,37)
 insert into spt_sybdrv values("char",129, 47)
 insert into spt_sybdrv values("varchar",129,39)
 insert into spt_sybdrv values("text",129,35) 
 insert into spt_sybdrv values("nchar",129,47)
 insert into spt_sybdrv values("nvarchar",129,39)
--insert into spt_sybdrv values("unitext",130,174)
 insert into spt_sybdrv values("unichar",130, 135)
 insert into spt_sybdrv values("univarchar",130, 155)
 insert into spt_sybdrv values("numeric",131,63)
 insert into spt_sybdrv values("decimal",131,55)
 insert into spt_sybdrv values("date",133,49)
 insert into spt_sybdrv values("time",134,51)
 insert into spt_sybdrv values("datetime",135,61)
 insert into spt_sybdrv values("smalldatetime",135,58)
-- insert into spt_sybdrv values("bigint",20,191)
-- insert into spt_sybdrv values("unsigned smallint",18,65)
-- insert into spt_sybdrv values("unsigned int",19,66)
-- insert into spt_sybdrv values("unsigned bigint",21,67)


end
 
 print "spt_sybdrv created!"
go


grant select on spt_sybdrv to public
go

if exists (select * from sysobjects where name = 'sp_oledb_computeprivs')
    begin
        drop procedure sp_oledb_computeprivs
    end
go



/*
** Results Table needs to be created so that sp_oledb_computeprivs has a temp
** table to reference when the procedure is compiled.  Otherwise, the calling
** stored procedure will create the temp table for sp_oledb_computeprivs.
*/
create table #results_table
    (table_qualifier        varchar (32),
     table_owner            varchar (32),
     table_name             varchar (32),
     column_name            varchar (32) NULL,
     grantor                varchar (32),
     grantee                varchar (32),
     privilege              varchar (32),
     is_grantable           varchar (3))
go

 

create procedure sp_oledb_computeprivs (
                        @table_name             varchar(32),
                        @table_owner            varchar(32),
                        @table_qualifier        varchar(32),
                        @column_name            varchar(32),
                        @calledfrom_colpriv     tinyint,
                        @tab_id                 int)

AS

/* Don't delete the following line. It is the checkpoint for sed */
/* Server dependent stored procedure add here ad ADDPOINT_COMPUTE_PRIVS */
--    declare @low 		int		/* range of userids to check */
--    declare @high 		int
--    declare @max_uid		smallint        /* max uid allowed for a user */

    declare @grantor_name       varchar (32)    /* the ascii name of grantor.
                                                   used for output */
    declare @grantee_name       varchar (32)    /* the ascii name of grantee.
                                                   used for output */
    declare @col_count          smallint        /* number of columns in
                                                   @table_name */
    declare @grantee            int             /* id of the grantee */
    declare @action             smallint        /* action refers to select,
                                                   update...*/
    declare @columns            varbinary (133) /* bit map of column
                                                   privileges */
    declare @protecttype        tinyint         /* grant/revoke or grant with
                                                   grant option */
    declare @grantor            int             /* id of the grantor of the
                                                   privilege */
    declare @grp_id             int             /* the group a user belongs
                                                   to */
    declare @grant_type         tinyint         /* used as a constant */
    declare @revoke_type        tinyint         /* used as a constant */
    declare @select_action      smallint        /* used as a constant */
    declare @update_action      smallint        /* used as a constant */
    declare @reference_action   smallint        /* used as a constant */
    declare @insert_action      smallint        /* used as a constant */
    declare @delete_action      smallint        /* used as a constant */
    declare @public_select      varbinary (133) /* stores select column bit map
                                                   for public */
    declare @public_reference   varbinary (133) /* stores reference column bit
                                                   map for public */
    declare @public_update      varbinary (133) /* stores update column bit map
                                                   for public */
    declare @public_insert      tinyint         /* stores if insert has been
                                                   granted to public */
    declare @public_delete      tinyint         /* store if delete has been
                                                   granted to public */
    declare @grp_select         varbinary (133) /* stores select column bit map
                                                   for group */
    declare @grp_update         varbinary (133) /* stores update column bit map
                                                   for group */
    declare @grp_reference      varbinary (133) /* stores reference column bit
                                                   map for group */
    declare @grp_delete         tinyint         /* if group hs been granted
                                                   delete privilege */
    declare @grp_insert         tinyint         /* if group has been granted
                                                   insert privilege */
    declare @inherit_select     varbinary (133) /* stores select column bit map
                                                   for inherited privs*/
    declare @inherit_update     varbinary (133) /* stores update column bit map
                                                   for inherited privs */
    declare @inherit_reference  varbinary (133) /* stores reference column bit
                                                   map for inherited privs */
    declare @inherit_insert     tinyint         /* inherited insert priv */
    declare @inherit_delete     tinyint         /* inherited delete priv */
    declare @select_go          varbinary (133) /* user column bit map of
                                                   select with grant */
    declare @update_go          varbinary (133) /* user column bit map of
                                                   update with grant */
    declare @reference_go       varbinary (133) /* user column bitmap of
                                                   reference with grant */
    declare @insert_go          tinyint         /* user insert priv with
                                                   grant option */
    declare @delete_go          tinyint         /* user delete priv with grant
                                                   option  */
    declare @prev_grantor       int             /* used to detect if the
                                                   grantor has changed between
                                                   two consecutive tuples */
    declare @col_pos            smallint        /* col_pos of the column we are
                                                   interested in. It is used to
                                                   find the col-bit in the
                                                   bitmap */
    declare @owner_id           int             /* id of the owner of the
                                                   table */
    declare @dbid               smallint        /* dbid for the table */
    declare @grantable          varchar (3)     /* 'YES' or 'NO' if the
                                                   privilege is grantable or
                                                   not */
    declare @is_printable       tinyint         /* 1, if the privilege info is
                                                   to be outputted */
     
     
     
    declare @startedInTransaction bit
    if (@@trancount > 0)
  	select @startedInTransaction = 1
    else
        select @startedInTransaction = 0
                                                       

    if (@startedInTransaction = 1)
	save transaction oledb_keep_temptable_tx    

/* 
** Initialize all constants to be used in this procedure
*/

    select @grant_type = 1

    select @revoke_type = 2
   
    select @select_action = 193

    select @reference_action = 151

    select @update_action = 197

    select @delete_action = 196

    select @insert_action = 195

--    select @max_uid = 16383

--    select @low = -32768, @high = 32767
/*
** compute the table owner id
*/

    select @owner_id = uid
    from   sysobjects
    where  id = @tab_id

/*



** create a temporary sysprotects table that only has grant/revoke tuples
** for the requested table. This is done as an optimization as the sysprotects
** table may need to be traversed several times
*/

    create table #sysprotects
        (uid            int,
         action         smallint,
         protecttype    tinyint,
         columns        varbinary (133) NULL,
         grantor        int)

/*
** This table contains all the groups including PUBLIC that users, who
** have been granted privilege on this table, belong to. Also it includes
** groups that have been explicitly granted privileges on the table object
*/
    create table #useful_groups
        (grp_id         int)

/*
** create a table that contains the list of grantors for the object requested.
** We will do a cartesian product of this table with sysusers in the
** current database to capture all grantor/grantee tuples
*/

    create table #distinct_grantors
        (grantor        int)

/*
** We need to create a table which will contain a row for every object
** privilege to be returned to the client.  
*/

    create table #column_privileges 
        (grantee_gid    int,
         grantor        int,
         grantee        int,
         insertpriv     tinyint,
         insert_go      tinyint NULL,
         deletepriv     tinyint,
         delete_go      tinyint NULL,
         selectpriv     varbinary (133) NULL,
         select_go      varbinary (133) NULL,
         updatepriv     varbinary (133) NULL,
         update_go      varbinary (133) NULL,
         referencepriv  varbinary (133) NULL,
         reference_go   varbinary (133) NULL)

/*
** this cursor scans the distinct grantor, group_id pairs
*/
    declare grp_cursor cursor for
        select distinct grp_id, grantor 
        from #useful_groups, #distinct_grantors
        order by grantor

/* 
** this cursor scans all the protection tuples that represent
** grant/revokes to users only
*/
    declare user_protect cursor for
        select uid, action, protecttype, columns, grantor
        from   #sysprotects
        where  (uid != 0) and
--              (uid <= @max_uid)
               ((uid >= @@minuserid and uid < @@mingroupid) or 
               (uid > @@maxgroupid and uid <= @@maxuserid)) 


/*
** this cursor is used to scan #column_privileges table to output results
*/
    declare col_priv_cursor cursor for
          select grantor, grantee, insertpriv, insert_go, deletepriv,
              delete_go, selectpriv, select_go, updatepriv, update_go,
              referencepriv, reference_go
          from #column_privileges



/*
** column count is needed for privilege bit-map manipulation
*/
    select @col_count = count (*) 
    from   syscolumns
    where  id = @tab_id


/* 
** populate the temporary sysprotects table #sysprotects
*/

        insert into #sysprotects 
                select uid, action, protecttype, columns, grantor
                from sysprotects
                where (id = @tab_id)               and
                      ((action = @select_action)   or
                      (action = @update_action)    or
                      (action = @reference_action) or
                      (action = @insert_action)    or
                      (action = @delete_action))

/* 
** insert privilege tuples for the table owner. There is no explicit grants
** of these privileges to the owner. So these tuples are not there in
** sysprotects table
*/

if not exists (select * from #sysprotects where (action = @select_action) and
                (protecttype = @revoke_type) and (uid = @owner_id))
begin
        insert into #sysprotects
             values (@owner_id, @select_action, 0, 0x01, @owner_id)
end

if not exists (select * from #sysprotects where (action = @update_action) and
                (protecttype = @revoke_type) and (uid = @owner_id))
begin
        insert into #sysprotects
             values (@owner_id, @update_action, 0, 0x01, @owner_id)
end

if not exists (select * from #sysprotects where (action = @reference_action) and
                (protecttype = @revoke_type) and (uid = @owner_id))
begin
        insert into #sysprotects
             values (@owner_id, @reference_action, 0, 0x01, @owner_id)
end

if not exists (select * from #sysprotects where (action = @insert_action) and
                (protecttype = @revoke_type) and (uid = @owner_id))
begin
        insert into #sysprotects
             values (@owner_id, @insert_action, 0, NULL, @owner_id)
end

if not exists (select * from #sysprotects where (action = @delete_action) and
                (protecttype = @revoke_type) and (uid = @owner_id))
begin
        insert into #sysprotects
             values (@owner_id, @delete_action, 0, NULL, @owner_id)
end


/* 
** populate the #distinct_grantors table with all grantors that have granted
** the privilege to users or to gid or to public on the table_name
*/

    insert into #distinct_grantors 
          select distinct grantor from #sysprotects

/* 
** Populate the #column_privilegs table as a cartesian product of the table
** #distinct_grantors and all the users, other than groups, in the current
** database
*/


    insert into #column_privileges
          select gid, g.grantor, su.uid, 0, 0, 0, 0, 0x00, 0x00, 0x00, 0x00,
              0x00, 0x00
          from sysusers su, #distinct_grantors g
        where  (su.uid != 0) and
--                (su.uid <= @max_uid)        
               ((su.uid >= @@minuserid and su.uid < @@mingroupid) or
               (su.uid > @@maxgroupid and su.uid <= @@maxuserid)) 

/*
** populate #useful_groups with only those groups whose members have been
** granted/revoked privileges on the @tab_id in the current database. It also
** contains those groups that have been granted/revoked privileges explicitly
*/

    insert into #useful_groups
        select distinct gid
        from   sysusers su, #sysprotects sp
        where  (su.uid = sp.uid) 


    open grp_cursor

    fetch grp_cursor into @grp_id, @grantor

    /* 
    ** This loop computes all the inherited privilegs of users due
    ** their membership in a group
    */

    while (@@sqlstatus != 2)
   
    begin

         /* 
         ** initialize variables 
         */
         select @public_select = 0x00
         select @public_update = 0x00
         select @public_reference = 0x00
         select @public_delete = 0
         select @public_insert = 0


         /* get the select privileges granted to PUBLIC */

         if (exists (select * from #sysprotects 
                     where (grantor = @grantor) and 
                           (uid = 0) and
                           (action = @select_action)))
         begin
              /* note there can't be any revoke row for PUBLIC */
              select @public_select = columns
              from #sysprotects
              where (grantor = @grantor) and 
                    (uid = 0) and
                    (action = @select_action)
         end


         /* get the update privilege granted to public */
         if (exists (select * from #sysprotects 
                     where (grantor = @grantor) and 
                           (uid = 0) and
                           (action = @update_action)))
         begin
              /* note there can't be any revoke row for PUBLIC */
              select @public_update = columns
              from #sysprotects
              where (grantor = @grantor) and 
                    (uid = 0) and
                    (action = @update_action)
         end

         /* get the reference privileges granted to public */
         if (exists (select * from #sysprotects 
                     where (grantor = @grantor) and 
                           (uid = 0) and
                           (action = @reference_action)))
         begin
              /* note there can't be any revoke row for PUBLIC */
              select @public_reference = columns
              from #sysprotects
              where (grantor = @grantor) and 
                    (uid = 0) and
                    (action = @reference_action)
         end


         /* get the delete privilege granted to public */
         if (exists (select * from #sysprotects 
                     where (grantor = @grantor) and 
                           (uid = 0) and
                           (action = @delete_action)))
         begin
              /* note there can't be any revoke row for PUBLIC */
              select @public_delete = 1
         end

         /* get the insert privileges granted to public */
         if (exists (select * from #sysprotects 
                     where (grantor = @grantor) and 
                           (uid = 0) and
                           (action = @insert_action)))
         begin
              /* note there can't be any revoke row for PUBLIC */
              select @public_insert = 1
         end


         /*
         ** initialize group privileges 
         */

         select @grp_select = 0x00
         select @grp_update = 0x00
         select @grp_reference = 0x00
         select @grp_insert = 0
         select @grp_delete = 0

         /* 
         ** if the group id is other than PUBLIC, we need to find the grants to
         ** the group also 
         */

         if (@grp_id <> 0)
         begin
                /* find select privilege granted to group */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @grant_type) and
                                  (action = @select_action)))
                begin
                        select @grp_select = columns
                        from #sysprotects
                        where (grantor = @grantor) and 
                              (uid = @grp_id) and
                              (protecttype = @grant_type) and 
                              (action = @select_action)
                end

                /* find update privileges granted to group */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @grant_type) and
                                  (action = @update_action)))
                begin
                        select @grp_update = columns
                        from #sysprotects
                        where (grantor = @grantor) and 
                              (uid = @grp_id) and
                              (protecttype = @grant_type) and 
                              (action = @update_action)
                end

                /* find reference privileges granted to group */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @grant_type) and
                                  (action = @reference_action)))
                begin
                        select @grp_reference = columns
                        from #sysprotects
                        where (grantor = @grantor) and 
                              (uid = @grp_id) and
                              (protecttype = @grant_type) and 
                              (action = @reference_action)
                end

                /* find delete privileges granted to group */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @grant_type) and
                                  (action = @delete_action)))
                begin

                        select @grp_delete = 1
                end

                /* find insert privilege granted to group */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @grant_type) and
                                  (action = @insert_action)))
                begin

                        select @grp_insert = 1

                end

         end

         /* at this stage we have computed all the grants to PUBLIC as well as
         ** the group by a specific grantor that we are interested in. Now we
         ** will use this info to compute the overall inherited privileges by
         ** the users due to their membership to the group or to PUBLIC 
         */


         exec sybsystemprocs.dbo.syb_aux_privunion @public_select, @grp_select,
             @col_count, @inherit_select output
         exec sybsystemprocs.dbo.syb_aux_privunion @public_update, @grp_update, 
             @col_count, @inherit_update output
         exec sybsystemprocs.dbo.syb_aux_privunion @public_reference,
             @grp_reference, @col_count, @inherit_reference output

         select @inherit_insert = @public_insert + @grp_insert
         select @inherit_delete = @public_delete + @grp_delete

         /*
         ** initialize group privileges to store revokes
         */

         select @grp_select = 0x00
         select @grp_update = 0x00
         select @grp_reference = 0x00
         select @grp_insert = 0
         select @grp_delete = 0

         /* 
         ** now we need to find if there are any revokes on the group under
         ** consideration. We will subtract all privileges that are revoked
         ** from the group from the inherited privileges
         */

         if (@grp_id <> 0)
         begin
                /* check if there is a revoke row for select privilege*/
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @revoke_type) and
                                  (action = @select_action)))
                begin
                        select @grp_select = columns
                        from #sysprotects
                        where (grantor = @grantor) and 
                              (uid = @grp_id) and
                              (protecttype = @revoke_type) and 
                              (action = @select_action)
                end

                /* check if there is a revoke row for update privileges */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @revoke_type) and
                                  (action = @update_action)))
                begin
                        select @grp_update = columns
                        from #sysprotects
                        where (grantor = @grantor) and 
                              (uid = @grp_id) and
                              (protecttype = @revoke_type) and 
                              (action = @update_action)
                end

                /* check if there is a revoke row for reference privilege */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @revoke_type) and
                                  (action = @reference_action)))
                begin
                        select @grp_reference = columns
                        from #sysprotects
                        where (grantor = @grantor) and 
                              (uid = @grp_id) and
                              (protecttype = @revoke_type) and 
                              (action = @reference_action)
                end

                /* check if there is a revoke row for delete privilege */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @revoke_type) and
                                  (action = @delete_action)))
                begin
                        select @grp_delete = 1
                end

                /* check if there is a revoke row for insert privilege */
                if (exists (select * from #sysprotects 
                            where (grantor = @grantor) and 
                                  (uid = @grp_id) and
                                  (protecttype = @revoke_type) and
                                  (action = @insert_action)))
                begin
                        select @grp_insert = 1

                end


                /* 
                ** now subtract the revoked privileges from the group
                */

                exec sybsystemprocs.dbo.syb_aux_privexor @inherit_select,
                                                 @grp_select,
                                                 @col_count,
                                                 @inherit_select output

                exec sybsystemprocs.dbo.syb_aux_privexor @inherit_update,
                                                 @grp_update,
                                                 @col_count,
                                                 @inherit_update output

                exec sybsystemprocs.dbo.syb_aux_privexor @inherit_reference,
                                                 @grp_reference,
                                                 @col_count,
                                                 @inherit_reference output

                if (@grp_delete = 1)
                        select @inherit_delete = 0

                if (@grp_insert = 1)
                        select @inherit_insert = 0

         end

         /*
         ** now update all the tuples in #column_privileges table for this
         ** grantor and group id
         */

         update #column_privileges
         set
                insertpriv      = @inherit_insert,
                deletepriv      = @inherit_delete,
                selectpriv      = @inherit_select,
                updatepriv      = @inherit_update,
                referencepriv   = @inherit_reference

         where (grantor     = @grantor) and
               (grantee_gid = @grp_id)


         /*
         ** the following update updates the privileges for those users
         ** whose groups have not been explicitly granted privileges by the
         ** grantor. So they will all have all the privileges of the PUBLIC
         ** that were granted by the current grantor
         */

         select @prev_grantor = @grantor         
         fetch grp_cursor into @grp_id, @grantor

         if ((@prev_grantor <> @grantor) or (@@sqlstatus = 2))

         begin
         /* Either we are at the end of the fetch or we are switching to
         ** a different grantor. 
         */

               update #column_privileges 
               set
                        insertpriv      = @public_insert,
                        deletepriv      = @public_delete,
                        selectpriv      = @public_select,
                        updatepriv      = @public_update,
                        referencepriv   = @public_reference
                from #column_privileges cp
                where (cp.grantor = @prev_grantor) and
                      (not EXISTS (select * 
                                   from #useful_groups ug
                                   where ug.grp_id = cp.grantee_gid))

         end
    end


    close grp_cursor


    /* 
    ** At this stage, we have populated the #column_privileges table with
    ** all the inherited privileges
    */
    /*
    ** update #column_privileges to give all access to the table owner that way
    ** if there are any revoke rows in sysprotects, then the calculations will
    ** be done correctly.  There will be no revoke rows for table owner if
    ** privileges are revoked from a group that the table owner belongs to.
    */
    update #column_privileges
    set
        insertpriv      = 0x01, 
        deletepriv      = 0x01, 
        selectpriv      = 0x01,
        updatepriv      = 0x01,
        referencepriv   = 0x01

        where grantor = grantee
          and grantor = @owner_id

    
    /* 
    ** Now we will go through each user grant or revoke in table #sysprotects
    ** and update the privileges in #column_privileges table
    */
    open user_protect

    fetch user_protect into @grantee, @action, @protecttype, @columns, @grantor

    while (@@sqlstatus != 2)
    begin
        /*
        ** In this loop, we can find grant row, revoke row or grant with grant
        ** option row. We use protecttype to figure that. If it is grant, then
        ** the user specific privileges are added to the user's inherited
        ** privileges. If it is a revoke, then the revoked privileges are
        ** subtracted from the inherited privileges. If it is a grant with
        ** grant option, we just store it as is because privileges can
        ** only be granted with grant option to individual users
        */

        /* 
        ** for select action
        */
        if (@action = @select_action)
        begin
             /* get the inherited select privilege */
             select @inherit_select = selectpriv
             from   #column_privileges
             where  (grantee = @grantee) and
                    (grantor = @grantor)

             if (@protecttype = @grant_type)
             /* the grantee has a individual grant */
                exec sybsystemprocs.dbo.syb_aux_privunion @inherit_select,
                    @columns, @col_count, @inherit_select output

             else 
                if (@protecttype = @revoke_type)
                /* it is a revoke row */
                     exec sybsystemprocs.dbo.syb_aux_privexor @inherit_select,
                         @columns, @col_count, @inherit_select output

                else
                     /* it is a grant with grant option */

                     select @select_go = @columns

             /* modify the privileges for this user */

             if ((@protecttype = @revoke_type) or (@protecttype = @grant_type))
             begin
                  update #column_privileges
                  set selectpriv = @inherit_select
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
             else
             begin

                  update #column_privileges
                  set select_go = @select_go
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
        end

        /*
        ** update action
        */
        if (@action = @update_action)
        begin
             /* find out the inherited update privilege */
             select @inherit_update = updatepriv
             from   #column_privileges
             where  (grantee = @grantee) and
                    (grantor = @grantor)


             if (@protecttype = @grant_type)
             /* user has an individual grant */
                exec sybsystemprocs.dbo.syb_aux_privunion @inherit_update,
                    @columns, @col_count, @inherit_update output

             else 
                if (@protecttype = @revoke_type)
                     exec sybsystemprocs.dbo.syb_aux_privexor @inherit_update,
                         @columns, @col_count, @inherit_update output

                else
                     /* it is a grant with grant option */
                     select @update_go = @columns


             /* modify the privileges for this user */

             if ((@protecttype = @revoke_type) or (@protecttype = @grant_type))
             begin
                  update #column_privileges
                  set updatepriv = @inherit_update
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
             else
             begin
                  update #column_privileges
                  set update_go = @update_go
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
        end

        /* it is the reference privilege */
        if (@action = @reference_action)
        begin
             select @inherit_reference = referencepriv
             from   #column_privileges
             where  (grantee = @grantee) and
                    (grantor = @grantor)


             if (@protecttype = @grant_type)
             /* the grantee has a individual grant */
                exec sybsystemprocs.dbo.syb_aux_privunion @inherit_reference,
                    @columns, @col_count, @inherit_reference output

             else 
                if (@protecttype = @revoke_type)
                /* it is a revoke row */
                     exec sybsystemprocs.dbo.syb_aux_privexor
                        @inherit_reference, @columns, @col_count,
                        @inherit_reference output

                else
                     /* it is a grant with grant option */
                     select @reference_go = @columns


             /* modify the privileges for this user */

             if ((@protecttype = @revoke_type) or (@protecttype = @grant_type))
             begin
                  update #column_privileges
                  set referencepriv = @inherit_reference
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
             else
             begin
                  update #column_privileges
                  set reference_go = @reference_go
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end

        end

        /*
        ** insert action
        */

        if (@action = @insert_action)
        begin
             if (@protecttype = @grant_type)
                   select @inherit_insert = 1
             else
                 if (@protecttype = @revoke_type)
                      select @inherit_insert = 0
                 else
                      select @insert_go = 1

             
             /* modify the privileges for this user */

             if ((@protecttype = @revoke_type) or (@protecttype = @grant_type))
             begin
                  update #column_privileges
                  set insertpriv = @inherit_insert
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
             else
             begin
                  update #column_privileges
                  set insert_go = @insert_go
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end

        end

        /* 
        ** delete action
        */

        if (@action = @delete_action)
        begin
             if (@protecttype = @grant_type)
                   select @inherit_delete = 1
             else
                 if (@protecttype = @revoke_type)
                      select @inherit_delete = 0
                 else
                      select @delete_go = 1

             
             /* modify the privileges for this user */

             if ((@protecttype = @revoke_type) or (@protecttype = @grant_type))
             begin
                  update #column_privileges
                  set deletepriv = @inherit_delete
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end
             else
             begin
                  update #column_privileges
                  set delete_go = @delete_go
                  where (grantor = @grantor) and
                        (grantee = @grantee)
             end

        end

        fetch user_protect into @grantee, @action, @protecttype, @columns,
            @grantor
    end

    close user_protect

open col_priv_cursor
fetch col_priv_cursor into @grantor, @grantee, @inherit_insert, @insert_go,
                         @inherit_delete, @delete_go, @inherit_select,
                         @select_go, @inherit_update, @update_go,
                         @inherit_reference, @reference_go

while (@@sqlstatus != 2)
begin

      /* 
      ** name of the grantor
      */
      select @grantor_name = name 
      from   sysusers
      where  uid = @grantor


      /*
      ** name of the grantee
      */

      select @grantee_name = name
      from   sysusers
      where  uid = @grantee

      /* 
      ** At this point, we are either printing privilege information for a
      ** a specific column or for table_privileges
      */

            select @col_pos = 0

            if (@calledfrom_colpriv = 1)
            begin
            /* 
            ** find the column position
            */
                 select @col_pos = colid
                 from syscolumns
                 where (id = @tab_id) and
                       (name = @column_name)
            end

            /* 
            ** check for insert privileges
            */
            /* insert privilege is only a table privilege */
            if (@calledfrom_colpriv = 0)
            begin
                    exec sybsystemprocs.dbo.syb_aux_printprivs 
                        @calledfrom_colpriv, @col_pos, @inherit_insert,
                        @insert_go, 0x00, 0x00, 0, @grantable output,
                        @is_printable output

                    if (@is_printable = 1)
                    begin
                          insert into #results_table
                               values (@table_qualifier, @table_owner,
                                       @table_name, @column_name,
                                       @grantor_name, @grantee_name, 'INSERT',
                                       @grantable)
                    end
            end

            /* 
            ** check for delete privileges
            */

            if (@calledfrom_colpriv = 0)
            /* delete privilege need only be printed if called from
               sp_table_privileges */
            begin
                    exec sybsystemprocs.dbo.syb_aux_printprivs 
                         @calledfrom_colpriv, @col_pos, @inherit_delete,
                         @delete_go, 0x00, 0x00, 0, @grantable output,
                         @is_printable output

                    if (@is_printable = 1)
                    begin
                        insert into #results_table
                                values (@table_qualifier, @table_owner,
                                        @table_name, @column_name,
                                        @grantor_name, @grantee_name, 'DELETE',
                                        @grantable)
                    end
            end

            /* 
            ** check for select privileges
            */
            exec sybsystemprocs.dbo.syb_aux_printprivs 
                        @calledfrom_colpriv, @col_pos, 0, 0, @inherit_select,
                        @select_go, 1, @grantable output, @is_printable output


            if (@is_printable = 1)
            begin
                  insert into #results_table
                         values (@table_qualifier, @table_owner, @table_name,
                                 @column_name, @grantor_name, @grantee_name,
                                 'SELECT', @grantable)
            end
            /* 
            ** check for update privileges
            */
            exec sybsystemprocs.dbo.syb_aux_printprivs 
                @calledfrom_colpriv, @col_pos, 0, 0, @inherit_update,
                @update_go, 1, @grantable output, @is_printable output

            if (@is_printable = 1)
            begin
                  insert into #results_table
                        values (@table_qualifier, @table_owner, @table_name,
                                @column_name, @grantor_name, @grantee_name,
                                'UPDATE', @grantable)
            end
            /*
            ** check for reference privs
            */
            exec sybsystemprocs.dbo.syb_aux_printprivs 
                @calledfrom_colpriv, @col_pos, 0, 0, @inherit_reference,
                @reference_go, 1, @grantable output, @is_printable output

            if (@is_printable = 1)
            begin
                insert into #results_table
                        values (@table_qualifier, @table_owner, @table_name,
                                @column_name, @grantor_name, @grantee_name,
                                'REFERENCE', @grantable)
            end



      fetch col_priv_cursor into @grantor, @grantee, @inherit_insert,
          @insert_go, @inherit_delete, @delete_go, @inherit_select, @select_go,
          @inherit_update, @update_go, @inherit_reference, @reference_go
end

close col_priv_cursor
    drop table #column_privileges
    drop table #distinct_grantors
    drop table #sysprotects
    drop table #useful_groups
   
if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx    

go

/*
** Drop temp table used for creation of sp_oledb_computeprivs
*/
drop table #results_table
go

exec sp_procxmode 'sp_oledb_computeprivs', 'anymode'
go

grant execute on sp_oledb_computeprivs to public
go


if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_datatype_info')
begin
	drop procedure sp_oledb_datatype_info
end
go

create procedure sp_oledb_datatype_info
@data_type int = 0,			/* Provide datatype_info for type # */
@best_match bit  = 0
as
declare @startedInTransaction bit

if (@@trancount > 0)
	select @startedInTransaction = 1
else
	select @startedInTransaction = 0

if (@startedInTransaction = 1)
 	save transaction oledb_keep_temptable_tx    

 create table #oledb_results_table
 (
  TYPE_NAME		varchar (32)  null,
  DATA_TYPE		smallint null,
  COLUMN_SIZE		int  null,
  LITERAL_PREFIX	varchar (32) null,
  LITERAL_SUFFIX		varchar (32) null,
  CREATE_PARAMS		varchar (32) null,
  IS_NULLABLE		bit,
  CASE_SENSITIVE	bit,
  SEARCHABLE		int null,
  UNSIGNED_ATTRIBUTE	bit,
  FIXED_PREC_SCALE	bit,
  AUTO_UNIQUE_VALUE	bit,
  LOCAL_TYPE_NAME	varchar (128) null,
  MINIMUM_SCALE 	smallint null,
  MAXIMUM_SCALE		smallint null,
  GUID			varchar (32) null,
  TYPELIB		varchar (32) null,
  VERSION		varchar (32) null,
  IS_LONG		bit,
  BEST_MATCH		bit,
  IS_FIXEDLENGTH	bit

  )

insert #oledb_results_table
select	/* Real SQL Server data types */
	TYPE_NAME = case
			when t.usertype in (44,45,46)
			then "unsigned "+substring(t.name,
			charindex("u",t.name) + 1,
			charindex("t",t.name))
		     else		
			t.name
		    end,
	DATA_TYPE = d.ss_dtype,
	COLUMN_SIZE = isnull(d.data_precision, 
			    convert(int,t.length)),
	LITERAL_PREFIX = d.literal_prefix,
	LITERAL_SUFFIX = d.literal_suffix,
	CREATE_PARAMS = e.create_params,
	IS_NULLABLE = convert(bit,d.nullable),
	CASE_SENSITIVE = d.case_sensitive,
	SEARCHABLE = d.searchable + 1,
	UNSIGNED_ATTRIBUTE = isnull(d.unsigned_attribute, convert(bit,1)),
	FIXED_PREC_SCALE = convert(bit,d.money),
	AUTO_UNIQUE_VALUE = isnull(d.auto_increment,convert(bit,0)),
	LOCAL_TYPE_NAME = d.local_type_name,
	MINIMUM_SCALE = d.minimum_scale,
	MAXIMUM_SCALE = d.maximum_scale,
	GUID = convert(varchar(32), null),
	TYPE_LIB =  convert(varchar(32), null),
	VERSION =  convert(varchar(32), null),
	ISLONG = convert(bit,0),
	BEST_MATCH = convert(bit,0),
	IS_FIXEDLENGTH = convert(bit,0)

from 	sybsystemprocs.dbo.spt_datatype_info d, syscolumns c,
	sybsystemprocs.dbo.spt_datatype_info_ext e, systypes t
where
	d.ss_dtype = t.type
	and t.usertype *= e.user_type
	    /* restrict results to "real" datatypes */
	and t.name not in ("nchar","nvarchar",
			   "sysname","longsysname","timestamp",
			   "datetimn","floatn","intn","uintn","moneyn",
			   "extended type")
	and t.usertype < 100	/* No user defined types */
UNION
select	/* SQL Server user data types */
	TYPE_NAME = t.name,
	DATA_TYPE = d.ss_dtype,
	COLUMN_SIZE = isnull(d.data_precision, 
			    convert(int,t.length)),
	LITERAL_PREFIX = d.literal_prefix,
	LITERAL_SUFFIX = d.literal_suffix,
	CREATE_PARAMS = e.create_params,
	IS_NULLABLE = convert(bit,d.nullable),
	CASE_SENSITIVE = d.case_sensitive,
	SEARCHABLE = d.searchable + 1,
	UNSIGNED_ATTRIBUTE = isnull(d.unsigned_attribute, convert(bit,1)),
	FIXED_PREC_SCALE = convert(bit,d.money),
	AUTO_UNIQUE_VALUE = isnull(d.auto_increment,convert(bit,0)),
	LOCAL_TYPE_NAME = t.name,
	MINIMUM_SCALE = d.minimum_scale,
	MAXIMUM_SCALE = d.maximum_scale,
	GUID = convert(varchar(32), null),
	TYPE_LIB =  convert(varchar(32), null),
	VERSION =  convert(varchar(32), null),
	ISLONG = convert(bit,0),
	BEST_MATCH = convert(bit,0),
	IS_FIXEDLENGTH = convert(bit,0)
from 	sybsystemprocs.dbo.spt_datatype_info d, syscolumns c,
	sybsystemprocs.dbo.spt_datatype_info_ext e, systypes t
where
	d.ss_dtype = t.type
	and t.usertype *= e.user_type
	    /* 
	    ** Restrict to user defined types (value > 100)
	    ** and Sybase user defined types (listed)
	    */
	and (t.name in ("nchar","nvarchar",
			    "sysname","timestamp")
	    and t.usertype < 100)      /* User defined types */
order by  TYPE_NAME
	
delete 	from #oledb_results_table where DATA_TYPE = 0	
update #oledb_results_table set o.DATA_TYPE = m.data_type   from
	sybsystemprocs.dbo.spt_sybdrv m, #oledb_results_table  o
	where o.TYPE_NAME =  m.type_name 

update #oledb_results_table set IS_FIXEDLENGTH = 1 where DATA_TYPE not in (37,39,35,155) and TYPE_NAME not in ("varchar", "varbinary", "univarchar", "text", "unitext", "image", "nvarchar")
update #oledb_results_table set IS_LONG = 1 where DATA_TYPE  in (34,35,174) or TYPE_NAME  in ("text",  "image", "unitext")


	
update #oledb_results_table set o.DATA_TYPE = m.data_type   from
	sybsystemprocs.dbo.spt_sybdrv m, #oledb_results_table  o
	where o.TYPE_NAME not in  (select type_name from sybsystemprocs.dbo.spt_sybdrv) and
	o.DATA_TYPE = m.tds_type

update #oledb_results_table set BEST_MATCH = 1 where DATA_TYPE not in (6, 128, 129, 130,131,135) 
update #oledb_results_table set BEST_MATCH = 1 where TYPE_NAME  = "money"
update #oledb_results_table set BEST_MATCH = 1 where TYPE_NAME  = "char"
update #oledb_results_table set BEST_MATCH = 1 where TYPE_NAME  = "unichar"
update #oledb_results_table set BEST_MATCH = 1 where TYPE_NAME  = "numeric"
update #oledb_results_table set BEST_MATCH = 1 where TYPE_NAME  = "binary"
update #oledb_results_table set BEST_MATCH = 1 where TYPE_NAME  = "datetime"

if(@data_type = 0)
select * from  #oledb_results_table order by DATA_TYPE, TYPE_NAME
else


if(@best_match = 0)
select * from  #oledb_results_table where DATA_TYPE = @data_type
else
select * from  #oledb_results_table where DATA_TYPE = @data_type 
and @best_match = 1

drop table  #oledb_results_table
if (@startedInTransaction = 1)
    rollback transaction oledb_keep_temptable_tx    

return (0)
go
exec sp_procxmode 'sp_oledb_datatype_info', 'anymode'
go
grant execute on sp_oledb_datatype_info to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go
print " Installed sp_oledb_datatype_info"
go

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_gettableprivileges')
begin
	drop procedure sp_oledb_gettableprivileges
end
go

/* Sccsid = "%Z% generic/sproc/src/%M% %I% %G%" */

create procedure sp_oledb_gettableprivileges ( 
                        @table_name  varchar(96) = null,
                        @table_schema varchar(32) = null,
                        @table_catalog varchar(32)= null,
                        @grantor varchar(32)= null,
                        @grantee varchar(32)= null)
as        
 
    declare @owner_id    		int
    declare @full_table_name    	varchar(193)
    declare @tab_id 			int	    /* object id of the table specified */

    declare @tab_name                   varchar(32)
    declare @tab_owner                  varchar(32)
    declare @table_id 			int	    /* object id of the
                                                       table specified */
    declare @startedInTransaction bit
    
    if (@@trancount > 0)
  	select @startedInTransaction = 1
    else
        select @startedInTransaction = 0

 
    set nocount on
    set transaction isolation level 1
    
    if (@startedInTransaction = 1)
 	save transaction oledb_keep_temptable_tx   
    

    /*
    **  Check to see that the table is qualified with the database name
    */
    if @table_name like "%.%.%"
    begin
	/* 18021, "Object name can only be qualified with owner name." */
	raiserror 18021
	return (1)
    end

    /*  If this is a temporary table; object does not belong to 
    **  this database; (we should be in our temporary database)
    */
    if (@table_name like "#%" and db_name() != 'tempdb')
    begin
	/* 
	** 17676, "This may be a temporary object. Please execute 
	** procedure from your temporary database."
	*/
	raiserror 17676
	return (1)
    end

   /*
   ** Results Table needs to be created so that sp_oledb_computeprivs has a temp
   ** table to reference when the procedure is compiled.  Otherwise, the calling
   ** stored procedure will create the temp table for sp_oledb_computeprivs.
   */
  create table #results_table
  	 (TABLE_CATALOG	varchar (32),
  	  TABLE_SCHEMA		varchar (32),
  	  TABLE_NAME		varchar (32),
  	  column_name		varchar (32) NULL,
  	  GRANTOR		varchar (32),
  	  GRANTEE 		varchar (32),
  	  PRIVILEGE_TYPE	varchar (32),
  	  IS_GRANTABLE		varchar (3))
  	  
    /*
    ** The table_catalog should be same as the database name. Do the sanity check
    ** if it is specified
    */
    if (@table_catalog is null) or (@table_catalog = '')
	/* set the table qualifier name */
	select @table_catalog = db_name ()
    else
    begin
        if db_name() != @table_catalog
        begin
	     /* 18039, "Table qualifier must be name of current database." */
		goto SelectFKClause
	end
    end
   
    /* 
    ** if the table owner is not specified, it will be taken as the id of the
    ** user executing this procedure. Otherwise find the explicit table name prefixed
    ** by the owner id
    */
    if (@table_schema is null) or (@table_schema = '')
        select @full_table_name = @table_name
    else
    begin
	if (@table_name like "%.%") and
	    substring (@table_name, 1, charindex(".", @table_name) -1) != @table_schema
	begin
	 	/* 18011, Object name must be qualified with the owner name */
		raiserror 18011
		return (1)
	end
	
	if not (@table_name like "%.%")
        	select @full_table_name = @table_schema + '.' + @table_name
	else
	        select @full_table_name = @table_name
    end

    /* 
    ** check to see if the specified table exists or not
    */

    select @tab_id = object_id(@full_table_name)
    if (@tab_id is null)
    begin
	/* 17492, "The table or view named doesn't exist in the current database." */
		goto SelectFKClause
    end


    /*
    ** check to see if the @tab_id indeeed represents a table or a view
    */

    if not exists (select * 
                  from   sysobjects
                  where (@tab_id = id) and
	                ((type = 'U') or
                        (type = 'S') or
		        (type = 'V')))
    begin
	/* 17492, "The table or view named doesn't exist in the current database." */
		goto SelectFKClause
    end

   /* 
   ** compute the table owner id
   */

    select @owner_id = uid
    from   sysobjects
    where  id = @tab_id



   /*
   ** get table owner name
   */

    select @table_schema = name 
    from sysusers 
    where uid = @owner_id

    /* Now, create a temporary table to hold a list of all the possible
       tables that we could get with the trio of table name, table owner and
       table catalog. Then, populate that table. */

    create table #odbc_tprivs
        (tab_id         int primary key,
         tab_name       varchar (32),
         tab_owner      varchar (32) null,
         uid            int,
         type           varchar (10))

    insert #odbc_tprivs 
        SELECT id, name, user_name(uid), uid, type 
        FROM sysobjects s 
        WHERE name LIKE @table_name ESCAPE '\'
            AND user_name (uid) LIKE @table_schema ESCAPE '\'
            AND charindex(substring(type,1,1), 'SUV') != 0

    declare tablepriv_cursor cursor for
        select tab_name, tab_owner, tab_id from #odbc_tprivs

    open tablepriv_cursor
   
    fetch tablepriv_cursor into @tab_name, @tab_owner, @table_id
 
    while (@@sqlstatus != 2)
    begin

        exec sp_oledb_computeprivs @tab_name, @tab_owner, @table_catalog, 
			     NULL, 0, @table_id
        fetch tablepriv_cursor into @tab_name, @tab_owner, @table_id

    end

    close tablepriv_cursor
    drop table #odbc_tprivs
      /* Output the results table */
    update #results_table set IS_GRANTABLE = '0' where IS_GRANTABLE="NO"
    update #results_table set IS_GRANTABLE = '1' where IS_GRANTABLE="YES"

SelectFKClause: 
    select GRANTOR, GRANTEE, TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME , 
    PRIVILEGE_TYPE = case when r.PRIVILEGE_TYPE = 'REFERENCE' then "REFERENCES"
      		        else r.PRIVILEGE_TYPE end,  
    IS_GRANTABLE=convert(bit, IS_GRANTABLE) from  #results_table r
	 order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, PRIVILEGE_TYPE
	 
 set nocount off 	
 drop table #results_table
 drop table #odbc_tprivs
if (@startedInTransaction = 1)
    rollback transaction oledb_keep_temptable_tx   

return (0)

go
exec sp_procxmode 'sp_oledb_gettableprivileges', 'anymode'
go
grant execute on sp_oledb_gettableprivileges to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go





if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_tables')
begin
	drop procedure sp_oledb_tables
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */
/*	10.0	1.1	06/16/93	sproc/tables */

/*
** Messages for "sp_oledb_tables"         18039
**
** 17676, "This may be a temporary object. Please execute procedure from tempdb."
**
** 18039, "Table qualifier must be name of current database"
**
*/
create procedure sp_oledb_tables
@table_catalog	 	varchar(32)  = null,
@table_schema     	varchar(32)  = null,
@table_name		varchar(96)  = null,
@table_type		varchar(100) = null
as

declare @type1 varchar(3)
declare @tableindex int
declare @startedInTransaction bit
if (@@trancount > 0)
   select @startedInTransaction = 1
else
   select @startedInTransaction = 0

if @@trancount = 0
begin
	set chained off
end


set transaction isolation level 1

if (@startedInTransaction = 1)
   save transaction oledb_keep_temptable_tx 


/* temp table */
if (@table_name like "#%" and
   db_name() != 'tempdb')
begin
	/*
        ** Can return data about temp. tables only in tempdb
        */
		raiserror 17676
	return(1)
end

create table #oledb_results_table
	 (
	  
	  TABLE_CATALOG		varchar (32) null,
	  TABLE_SCHEMA		varchar (32) null,
	  TABLE_NAME		varchar (32) null,
	  TABLE_TYPE		varchar (32) null,
	  TABLE_GUID		varchar	(32) null,
	  DESCRIPTION		varchar(32) null,
	  TABLE_PROPID		int null,
	  DATE_CREATED		datetime null,
	  DATE_MODIFIED		datetime null
	 )

/*
** Special feature #1:	enumerate databases when owner and name
** are blank but qualifier is explicitly '%'.  
*/
if @table_catalog = '%' and
	@table_schema = '' and
	@table_name = ''
begin	

	/*
	** If enumerating databases 
	*/
	insert #oledb_results_table
	select
		TABLE_CATALOG = name,
		TABLE_SCHEMA = null,
		TABLE_NAME = null,
		TABLE_TYPE = 'Database',
		TABLE_GUID = convert(varchar(32), null),
		DESCRIPTION = convert(varchar(32), null),
		TABLE_PROPID = convert(int, null),
		DATE_CREATED = convert(datetime, null),
	  	DATE_MODIFIED = convert(datetime, null)

		
		from master..sysdatabases

		/*
		** eliminate MODEL database 
		*/
		where name != 'model'
		order by TABLE_CATALOG
end

/*
** Special feature #2:	enumerate owners when qualifier and name
** are blank but owner is explicitly '%'.
*/
else if @table_catalog = '' and
	@table_schema = '%' and
	@table_name = ''
	begin	

		/*
		** If enumerating owners 
		*/
		insert #oledb_results_table
		select distinct
			TABLE_CATALOG = null,
			TABLE_SCHEMA = user_name(uid),
			TABLE_NAME = null,
			TABLE_TYPE = 'Owner',
			TABLE_GUID = convert(varchar(32), null),
			DESCRIPTION = convert(varchar(32), null),
			TABLE_PROPID = convert(int, null),
			DATE_CREATED = convert(datetime, null),
			DATE_MODIFIED = convert(datetime, null)


		

		from sysobjects
		order by TABLE_SCHEMA
	end
	else
	begin 

		/*
		** end of special features -- do normal processing 
		*/
		if @table_catalog is not null
	
begin
			if db_name() != @table_catalog
			begin
				if @table_catalog = ''
				begin  	

					/*
					** If empty qualifier supplied
					** Force an empty result set 
					*/
					select @table_name = ''
					select @table_schema = ''
				end
				else
				begin

					/*
					** If qualifier doesn't match current 
					** database. 
					*/
					raiserror 18039
					return 1
				end
			end
		end
		if @table_type is null
	
begin	

			/*
			** Select all oledb supported table types 
			*/
			select @type1 = 'SUV'
		end
		else
		begin
			/*
			** TableType are case sensitive if CS server 
			*/
			select @type1 = null

		
/*
			** Add System Tables 
			*/
			if (patindex("%SYSTEM TABLE%",@table_type) != 0)
				select @type1 =  'S'

			/*
			** Add User Tables 
			*/
			if (patindex("%TABLE%",@table_type) != 0)
				select @type1 = @type1 + 'U'

			/*
			** Add Views 
			*/
			if (patindex("%VIEW%",@table_type) != 0)
				select @type1 = @type1 + 'V'
		end
		if @table_name is null
	
begin	

			/*
			** If table name not supplied, match all 
			*/
			select @table_name = '%'
		end
		else
		begin
			if (@table_schema is null) and 
			   (charindex('%', @table_name) = 0)
			begin	

			/*
			** If owner not specified and table is specified 
			*/
				if exists (select * from sysobjects
					where uid = user_id()
					and id = object_id(@table_name)
					and (type = 'U' or type = 'V' 
						or type = 'S'))
				begin	

				/*
				** Override supplied owner w/owner of table 
				*/
					select @table_schema = user_name()
				end
			end
		end

		/*
		** If no owner supplied, force wildcard 
		*/
		if @table_schema is null 
		 select @table_schema = '%'
		insert #oledb_results_table
		select
			TABLE_CATALOG = db_name(),
			TABLE_SCHEMA = user_name(o.uid),
			TABLE_NAME = o.name,
			TABLE_TYPE = rtrim ( 
					substring('SYSTEM TABLE            TABLE       VIEW       ',
					/*
					** 'S'=0,'U'=2,'V'=3 
					*/
					(ascii(o.type)-83)*12+1,12)),

			
			TABLE_GUID  = convert(varchar(32), null),
			DESCRIPTION = convert(varchar(32),null),
			TABLE_PROPID = convert(int,null),
			DATE_CREATED = convert(datetime, null),
			DATE_MODIFIED = convert(datetime, null)

		from sysusers u, sysobjects o
		where
			/* Special case for temp. tables.  Match ids */
			(o.name like @table_name or o.id=object_id(@table_name))
			and user_name(o.uid) like @table_schema

			/*
			** Only desired types
			*/
			and charindex(substring(o.type,1,1),@type1)! = 0 

			/*
			** constrain sysusers uid for use in subquery 
			*/
			and u.uid = user_id() 
		and (
                suser_id() = 1          /* User is the System Administrator */
                or o.uid = user_id()    /* User created the object */
                                        /* here's the magic..select the highest
                                        ** precedence of permissions in the
                                        ** order (user,group,public)
                                        */
 
                /*
                ** The value of protecttype is
                **
                **      0  for grant with grant
                **      1  for grant and,
                **      2  for revoke
                **
                ** As protecttype is of type tinyint, protecttype/2 is
                ** integer division and will yield 0 for both types of
                ** grants and will yield 1 for revoke, i.e., when
                ** the value of protecttype is 2.  The XOR (^) operation
                ** will reverse the bits and thus (protecttype/2)^1 will
                ** yield a value of 1 for grants and will yield a
                ** value of zero for revoke.
                **
	        ** For groups, uid = gid. We shall use this to our advantage.
                **
                ** If there are several entries in the sysprotects table
                ** with the same Object ID, then the following expression
                ** will prefer an individual uid entry over a group entry
                **
                ** For example, let us say there are two users u1 and u2
                ** with uids 4 and 5 respectiveley and both u1 and u2
                ** belong to a group g12 whose uid is 16390.  table t1
                ** is owned by user u0 and user u0 performs the following
                ** actions:
                **
                **      grant select on t1 to g12
                **      revoke select on t1 from u1
                **
                ** There will be two entries in sysprotects for the object t1,
                ** one for the group g12 where protecttype = grant (1) and
                ** one for u1 where protecttype = revoke (2).
                **
                ** For the group g12, the following expression will
                ** evaluate to:
                **
                **      ((abs(16390-16390)*2) + ((1/2)^1)
                **      = ((0) + (0)^1) = 0 + 1 = 1
                **
                ** For the user entry u1, it will evaluate to:
                **
                **      (((+)*abs(4-16390)*2) + ((2/2)^1))
                **      = (abs(-16386)*2 + (1)^1)
                **      = 16386*2 + 0 = 32772
                **
                ** As the expression evaluates to a bigger number for the
                ** user entry u1, select max() will chose 32772 which,
                ** ANDed with 1 gives 0, i.e., sp_oledb_tables will not display
                ** this particular table to the user.
                **
                ** When the user u2 invokes sp_oledb_tables, there is only one
                ** entry for u2, which is the entry for the group g12, and
                ** so the group entry will be selected thus allowing the
                ** table t1 to be displayed.
                **
		** ((select max((abs(uid-u.gid)*2)
	        ** 		+ ((protecttype/2)^1))
         	**
                ** Notice that multiplying by 2 makes the number an
                ** even number (meaning the last digit is 0) so what
                ** matters at the end is (protecttype/2)^1.
                **
                **/
 
                or ((select max((abs(p.uid-u2.gid)*2) + ((p.protecttype/2)^1))
                        from sysprotects p, sysusers u2
                        where p.id = o.id      /* outer join to correlate
                                                ** with all rows in sysobjects
                                                */
			and u2.uid = user_id()
			/*
			** get rows for public, current users, user's groups
			*/
		      	and (p.uid = 0 or 		/* public */
			     p.uid = user_id() or	/* current user */ 
			     p.uid = u2.gid)		/* users group */ 

			/*
			** check for SELECT, EXECUTE privilege.
			*/
		 	and (p.action in (193,224)))&1

			/*
			** more magic...normalise GRANT
			** and final magic...compare
			** Grants.
			*/
			) = 1
		/*
			** If one of any user defined roles or contained roles for the
			** user has permission, the user has the permission
			*/
			or exists(select 1
				from sysprotects p1,
					master.dbo.syssrvroles srvro,
					sysroles ro
				where p1.id = o.id
				and p1.uid = ro.lrid
				and ro.id = srvro.srid
	--	and has_role(srvro.name, 1) > 0
				and p1.action = 193))
		
		order by TABLE_TYPE, TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME
end
if (patindex("%VIEW%",@table_type) != 0)
	select TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, null, null, null, 
	DESCRIPTION, DATE_CREATED, DATE_MODIFIED from #oledb_results_table
else
	select * from #oledb_results_table
drop table #oledb_results_table
if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx

return (0)
go
exec sp_procxmode 'sp_oledb_tables', 'anymode'
go
grant execute on sp_oledb_tables to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_statistics')
begin
	drop procedure sp_oledb_statistics
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */
/*	10.0	1.1	06/16/93	sproc/tables */

/*
** Messages for "sp_oledb_statistics"         18039
**
** 17676, "This may be a temporary object. Please execute procedure from tempdb."
**
** 18039, "Table qualifier must be name of current database"
**
*/
create procedure sp_oledb_statistics
@table_catalog	 	varchar(32)  = null,
@table_schema     	varchar(32)  = null,
@table_name		varchar(32)  = null

as

declare @type1 varchar(3)
declare @tname varchar(32)
declare @startedInTransaction bit

if (@@trancount > 0)
   select @startedInTransaction = 1
else
   select @startedInTransaction = 0

if @@trancount = 0
begin
	set chained off
end

set transaction isolation level 1

if (@startedInTransaction = 1)
 save transaction oledb_keep_temptable_tx

/* temp table */
if (@table_name like "#%" and
   db_name() != 'tempdb')
begin
	/*
        ** Can return data about temp. tables only in tempdb
        */
		raiserror 17676
	return(1)
end

create table #oledb_results_table
	 (
	  
	  TABLE_CATALOG		varchar (32) null,
	  TABLE_SCHEMA		varchar (32) null,
	  TABLE_NAME		varchar (32) null,
	  CARDINALITY		int null
	 )
	  		 	
		 		
     				
/*
** Special feature #1:	enumerate databases when owner and name
** are blank but catalog is explicitly '%'.  
*/
if @table_catalog = '%' and
	@table_schema = '' and
	@table_name = ''
begin	

	/*
	** If enumerating databases 
	*/
	insert #oledb_results_table
	select
		TABLE_CATALOG = name,
		TABLE_SCHEMA = null,
		TABLE_NAME = null,
		CARDINALITY = null
		from master..sysdatabases

		/*
		** eliminate MODEL database 
		*/
		where name != 'model'
		order by TABLE_CATALOG
end

/*
** Special feature #2:	enumerate owners when qualifier and name
** are blank but owner is explicitly '%'.
*/
else if @table_catalog = '' and
	@table_schema = '%' and
	@table_name = ''
	begin	

		/*
		** If enumerating owners 
		*/
		insert #oledb_results_table
		select distinct
			TABLE_CATALOG = null,
			TABLE_SCHEMA = user_name(uid),
			TABLE_NAME = null,
			CARDINALITY = null
		from sysobjects
		order by TABLE_CATALOG
	end
	else
	begin 

		/*
		** end of special features -- do normal processing 
		*/
		if @table_catalog is not null
	
		begin
			if db_name() != @table_catalog
			begin
				if @table_catalog = ''
				begin  	

					/*
					** If empty qualifier supplied
					** Force an empty result set by 
					** going directly to select
					*/
					goto SelectClause
				end
			end
		end
		
		select @type1 = 'SUV'
	
		
		if @table_name is null
	
		begin	

			/*
			** If table name not supplied, match all 
			*/
			select @table_name = '%'
		end
		else
		begin
			if (@table_schema is null) and 
			   (charindex('%', @table_name) = 0)
			begin	

			/*
			** If owner not specified and table is specified 
			*/
				if exists (select * from sysobjects
					where uid = user_id()
					and id = object_id(@table_name)
					and (type = 'U' or type = 'V' 
						or type = 'S'))
				begin	

				/*
				** Override supplied owner w/owner of table 
				*/
					select @table_schema = user_name()
				end
			end
		end

		/*
		** If no owner supplied, force wildcard 
		*/
		if @table_schema is null 
		 select @table_schema = '%'
		/*
		** If no catalog supplied, force wildcard 
		*/
		if @table_catalog is null 
		 select @table_catalog = '%'
		 
		
		insert #oledb_results_table
		select
			TABLE_CATALOG = db_name(),
			TABLE_SCHEMA = user_name(o.uid),
			TABLE_NAME = o.name,
		CARDINALITY = rowcnt(x.doampg)
--		CARDINALITY = row_count(db_id(), x.id)

			from sysusers u, sysobjects o, sysindexes x
		where
			/* Special case for temp. tables.  Match ids */
			(o.name like @table_name or o.id=object_id(@table_name))
			and user_name(o.uid) like @table_schema

			/*
			** Only desired types
			*/
			and charindex(substring(o.type,1,1),@type1)! = 0 
			and o.name = x.name

			/*
			** constrain sysusers uid for use in subquery 
			*/
			and u.uid = user_id() 
		and (
                suser_id() = 1          /* User is the System Administrator */
                or o.uid = user_id()    /* User created the object */
                                        /* here's the magic..select the highest
                                        ** precedence of permissions in the
                                        ** order (user,group,public)
                                        */
 
                /*
                ** The value of protecttype is
                **
                **      0  for grant with grant
                **      1  for grant and,
                **      2  for revoke
                **
                ** As protecttype is of type tinyint, protecttype/2 is
                ** integer division and will yield 0 for both types of
                ** grants and will yield 1 for revoke, i.e., when
                ** the value of protecttype is 2.  The XOR (^) operation
                ** will reverse the bits and thus (protecttype/2)^1 will
                ** yield a value of 1 for grants and will yield a
                ** value of zero for revoke.
                **
	        ** For groups, uid = gid. We shall use this to our advantage.
                **
                ** If there are several entries in the sysprotects table
                ** with the same Object ID, then the following expression
                ** will prefer an individual uid entry over a group entry
                **
                ** For example, let us say there are two users u1 and u2
                ** with uids 4 and 5 respectiveley and both u1 and u2
                ** belong to a group g12 whose uid is 16390.  table t1
                ** is owned by user u0 and user u0 performs the following
                ** actions:
                **
                **      grant select on t1 to g12
                **      revoke select on t1 from u1
                **
                ** There will be two entries in sysprotects for the object t1,
                ** one for the group g12 where protecttype = grant (1) and
                ** one for u1 where protecttype = revoke (2).
                **
                ** For the group g12, the following expression will
                ** evaluate to:
                **
                **      ((abs(16390-16390)*2) + ((1/2)^1)
                **      = ((0) + (0)^1) = 0 + 1 = 1
                **
                ** For the user entry u1, it will evaluate to:
                **
                **      (((+)*abs(4-16390)*2) + ((2/2)^1))
                **      = (abs(-16386)*2 + (1)^1)
                **      = 16386*2 + 0 = 32772
                **
                ** As the expression evaluates to a bigger number for the
                ** user entry u1, select max() will chose 32772 which,
                ** ANDed with 1 gives 0, i.e., sp_oledb_statistics will not display
                ** this particular table to the user.
                **
                ** When the user u2 invokes sp_oledb_statistics, there is only one
                ** entry for u2, which is the entry for the group g12, and
                ** so the group entry will be selected thus allowing the
                ** table t1 to be displayed.
                **
		** ((select max((abs(uid-u.gid)*2)
	        ** 		+ ((protecttype/2)^1))
         	**
                ** Notice that multiplying by 2 makes the number an
                ** even number (meaning the last digit is 0) so what
                ** matters at the end is (protecttype/2)^1.
                **
                **/
 
                or ((select max((abs(p.uid-u2.gid)*2) + ((p.protecttype/2)^1))
                        from sysprotects p, sysusers u2
                        where p.id = o.id      /* outer join to correlate
                                                ** with all rows in sysobjects
                                                */
			and u2.uid = user_id()
			/*
			** get rows for public, current users, user's groups
			*/
		      	and (p.uid = 0 or 		/* public */
			     p.uid = user_id() or	/* current user */ 
			     p.uid = u2.gid)		/* users group */ 

			/*
			** check for SELECT, EXECUTE privilege.
			*/
		 	and (p.action in (193,224)))&1

			/*
			** more magic...normalise GRANT
			** and final magic...compare
			** Grants.
			*/
			) = 1)
		order by  TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME
		
end

SelectClause:       
select * from #oledb_results_table where TABLE_CATALOG like @table_catalog 
AND TABLE_SCHEMA like @table_schema
AND TABLE_NAME like @table_name order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME

drop table #oledb_results_table
if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx 

return (0)
go
exec sp_procxmode 'sp_oledb_statistics', 'anymode'
go
grant execute on sp_oledb_statistics to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go


if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_stored_procedures')
begin
	drop procedure sp_oledb_stored_procedures
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */

/*
** Messages for "sp_oledb_stored_procedures"	18041
**
** 18041, "Stored Procedure qualifier must be name of current database."
**
*/
create procedure sp_oledb_stored_procedures
@sp_name	varchar(96) = null,	/* stored procedure name */
@sp_owner	varchar(32) = null,	/* stored procedure owner */
@sp_qualifier	varchar(32) = null,	/* stored procedure qualifier; 
					** For the SQL Server, the only valid
					** values are NULL or the current 
					** database name
					*/
@type varchar(2)= null,		/* used for ado.net2 and do nothing here */		
@is_ado    int = 1
	
					
as


if @@trancount = 0
begin
	set chained off
end

set transaction isolation level 1

/* If qualifier is specified */
if @sp_qualifier is not null
begin
	/* If qualifier doesn't match current database */
	if db_name() != @sp_qualifier
	begin
		/* If qualifier is not specified */
		if @sp_qualifier = ''
		begin
			/* in this case, we need to return an empty 
			** result set because the user has requested a 
			** database with an empty name 
			*/
			select @sp_name = ''
			select @sp_owner = ''
		end

		/* qualifier is specified and does not match current database */
		else
		begin	
			/* 
			** 18041, "Stored Procedure qualifer must be name of
			** current database"
			*/
			raiserror 18041
			return (1)
		end
	end
end

/* If procedure name not supplied, match all */
if @sp_name is null
begin  
	select @sp_name = '%'
end
else 
begin
	/* If owner name is not supplied, but procedure name is */ 
	if (@sp_owner is null) and (charindex('%', @sp_name) = 0)
	begin
		/* If procedure exists and is owned by the current user */
		if exists (select * 
			   from sysobjects
			   where uid = user_id()
				and name = @sp_name
				and type = 'P') /* Object type of Procedure */
		begin
			/* Set owner name to current user */
			select @sp_owner = user_name()
		end
	end
end

/* If procedure owner not supplied, match all */
if @sp_owner is null	
	select @sp_owner = '%'

/* 
** Retrieve the stored procedures and associated info on them
*/
select  PROCEDURE_CATALOG = db_name(),
	PROCEDURE_SCHEMA = user_name(o.uid),
	PROCEDURE_NAME = o.name +';'+ ltrim(str(p.number,5)),
	PROCEDURE_TYPE = case when o.type='P' then convert(smallint, 1) when o.type='F' then convert(smallint, 2) end,
	PROCEDURE_DEFINITION= convert(varchar(254),null),
	DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
	DATE_CREATED = case when @is_ado = 2 then convert(datetime, o.crdate) when @is_ado = 1 then convert(datetime, null) end,
	DATE_MODIFIED = case when @is_ado = 2 then convert(datetime,o.expdate) when @is_ado = 1 then convert(datetime, null) end
from sysobjects o,sysprocedures p,sysusers u
where o.name like @sp_name
	and p.sequence = 0
	and user_name(o.uid) like @sp_owner
	and o.type in ('P','F')  /* Object type of Procedure or Function */
	and p.id = o.id
	and u.uid = user_id()		/* constrain sysusers uid for use in 
					** subquery 
					*/

	and (suser_id() = 1 		/* User is the System Administrator */
	     or  o.uid = user_id()	/* User created the object */
					/* here's the magic..select the highest 
					** precedence of permissions in the 
					** order (user,group,public)  
					*/

	     /*
	     ** The value of protecttype is
	     **
	     **		0  for grant with grant
	     **		1  for grant and,
	     **		2  for revoke
	     **
	     ** As protecttype is of type tinyint, protecttype/2 is
	     ** integer division and will yield 0 for both types of
	     ** grants and will yield 1 for revoke, i.e., when
	     ** the value of protecttype is 2.  The XOR (^) operation
	     ** will reverse the bits and thus (protecttype/2)^1 will
	     ** yield a value of 1 for grants and will yield a
	     ** value of zero for revoke.
	     **
	     ** For groups, uid = gid. We shall use this to our advantage.
             ** 	
	     ** If there are several entries in the sysprotects table
	     ** with the same Object ID, then the following expression
	     ** will prefer an individual uid entry over a group entry
	     **
	     ** For example, let us say there are two users u1 and u2
	     ** with uids 4 and 5 respectiveley and both u1 and u2
	     ** belong to a group g12 whose uid is 16390.  procedure p1
	     ** is owned by user u0 and user u0 performs the following
	     ** actions:
	     **
	     **		grant exec on p1 to g12
	     **		revoke grant on p1 from u1
	     **
	     ** There will be two entries in sysprotects for the object
	     ** p1, one for the group g12 where protecttype = grant (1)
	     ** and one for u1 where protecttype = revoke (2).
	     **
	     ** For the group g12, the following expression will
	     ** evaluate to:
	     **
	     **		((abs(16390-16390)*2) + ((1/2)^1))
	     **		= ((0) + (0)^1) = 0 + 1 = 1
	     **
	     ** For the user entry u1, it will evaluate to:
	     **
	     **		((abs(4-16390)*2) + ((2/2)^1))
	     **		= ((abs(-16386)*2 + (1)^1)
	     **		= 16386*2 + 0 = 32772 
	     **
	     ** As the expression evaluates to a bigger number for the
	     ** user entry u1, select max() will chose 32772 which,
	     ** ANDed with 1 gives 0, i.e., sp_oledb_stored_procedures will
	     ** not display this particular procedure to the user.
	     **
	     ** When the user u2 invokes sp_oledb_stored_procedures, there is
	     ** only one entry for u2, which is the entry for the group
	     ** g12, and so this entry will be selected thus allowing
	     ** the procedure in question to be displayed.
	     **
             ** NOTE: With the extension of the uid's into negative space, 
             ** and uid limits going beyond 64K, the original expression 
	     ** has been modified from
	     ** ((select max(((sign(uid)*abs(uid-16383))*2)
	     **		+ ((protecttype/2)^1))
	     ** to
	     ** ((select max((abs(uid-u.gid)*2)
	     **		+ ((protecttype/2)^1))
	     ** 
	     ** Notice that multiplying by 2 makes the number an
	     ** even number (meaning the last digit is 0) so what
	     ** matters at the end is (protecttype/2)^1.
	     **
	     */

	     or ((select max( (abs(p.uid-u2.gid)*2) + ((p.protecttype/2)^1))
		   from sysprotects p, sysusers u2
		   where p.id = o.id
		   and u2.uid = user_id()
		   /*
		   ** get rows for public, current users, user's groups
		   */
		   and (p.uid = 0  		/* get rows for public */
		        or p.uid = user_id()	/* current user */
		        or p.uid = u2.gid) 	/* users group */
			     
		   /*
		   ** check for SELECT, EXECUTE privilege.
		   */
	           and (p.action in (193,224))	/* check for SELECT,EXECUTE 
						** privilege 
						*/
		   )&1 			/* more magic...normalize GRANT */
	    	  ) = 1	 		/* final magic...compare Grants	*/
	     /*
	     ** If one of any user defined roles or contained roles for the
	     ** user has permission, the user has the permission
	     */
	     or exists(select 1
		from	sysprotects p1,
			master.dbo.syssrvroles srvro,
			sysroles ro
		where	p1.id = o.id
			and p1.uid = ro.lrid
			and ro.id = srvro.srid
--	and has_role(srvro.name, 1) > 0		
			and p1.action = 224))
			
order by PROCEDURE_CATALOG, PROCEDURE_SCHEMA, PROCEDURE_NAME
go
exec sp_procxmode 'sp_oledb_stored_procedures', 'anymode'
go
grant execute on sp_oledb_stored_procedures to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go







if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_getprocedurecolumns')
begin
	drop procedure sp_oledb_getprocedurecolumns
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */

/*
** Messages for "sp_oledb_getprocedurecolumns"
**
** 18039, "Table qualifier must be name of current database"
*/

create procedure sp_oledb_getprocedurecolumns
@procedure_name		varchar(96) = '%', 	/* name of stored procedure  */
@procedure_schema	varchar(32) = null,	/* owner of stored procedure */
@procedure_catalog	varchar(32) = null,	/* name of current database  */
@parameter_name		varchar(96) = null,	/* col name or param name    */
@is_ado             int = 1
as

declare @msg 		  varchar(32)
declare @group_num		int
declare @semi_position		int
declare @full_procedure_name	varchar(193)
declare @procedure_id		int
declare @char_bin_types   varchar(30) 
declare @sptlang 			int
declare @startedInTransaction bit
if (@@trancount > 0)
   select @startedInTransaction = 1
else
   select @startedInTransaction = 0

if @@trancount = 0
begin
	set chained off
end

set transaction isolation level 1

if (@startedInTransaction = 1)
 save transaction oledb_keep_temptable_tx
 
select @sptlang = @@langid

 if @@langid != 0
 begin
         if not exists (
                 select * from master.dbo.sysmessages where error
                 between 17100 and 17109
                 and langid = @@langid)
             select @sptlang = 0
 end
 
create table #oledb_results_table
(
  	PROCEDURE_CATALOG  varchar (32) null,
  	PROCEDURE_SCHEMA  varchar (32) null,
  	PROCEDURE_NAME  varchar (32) null,
  	PARAMETER_NAME  varchar (32) null,
  	ORDINAL_POSITION	smallint null,
  	PARAMETER_TYPE 		smallint null,
  	PARAMETER_HASDEFAULT	bit ,
  	PARAMETER_DEFAULT	varchar (32) null,
  	IS_NULLABLE		bit ,
  	DATA_TYPE		smallint null,
  	CHARACTER_MAXIMUM_LENGTH	int null,
  	CHARACTER_OCTET_LENGTH		int null,
  	NUMERIC_PRECISION		smallint null,
  	NUMERIC_PRECISION_RADIX  smallint null,
  	NUMERIC_SCALE			smallint null,
  	DESCRIPTION		varchar(32) null,
  	TYPE_NAME		varchar (32) null,
  	LOCAL_TYPE_NAME		varchar (32) null,
  	
)

/* If column name not supplied, match all */
if @parameter_name is null 
	select @parameter_name = '%'

/* The qualifier must be the name of current database or null */
if @procedure_catalog is not null
begin
	if db_name() != @procedure_catalog
	begin
	if @procedure_catalog = ''
		begin
			/* in this case, we need to return an empty result 
			** set because the user has requested a database with
			** an empty name
			*/
			select @procedure_name = ''
			select @procedure_schema= ''
		end
		else
		begin
			/*
			** 18039, Table qualifier must be name of current database
			*/
			exec sp_getmessage 18039, @msg output
			print @msg
			return
		end
	end
end


/* first we need to extract the procedure group number, if one exists */
select @semi_position = charindex(';',@procedure_name)
if (@semi_position > 0)
begin	/* If group number separator (;) found */
	select @group_num = convert(int,substring(@procedure_name, 
						  @semi_position + 1, 2))
	select @procedure_name = substring(@procedure_name, 1, 
					   @semi_position -1)
end
else
begin	/* No group separator, so default to group number of 1 */
	select @group_num = 1
end

/* character and binary datatypes */
select @char_bin_types =
	char(47)+char(39)+char(45)+char(37)+char(35)+char(34)

if @procedure_schema is null
begin	/* If unqualified procedure name */
	select @full_procedure_name = @procedure_name
end
else
begin	/* Qualified procedure name */
	select @full_procedure_name = @procedure_schema+ '.' + @procedure_name
end

/*
** If the @parameter_name parameter is "RETURN_VALUE" and this is a sqlj
** function, then we should be looking for column name "Return Type"
*/
if @parameter_name = "RETURN_VALUE"
	and exists (select 1 from sysobjects
		    where id = object_id(@full_procedure_name)
		    and type = 'F')
begin	
	select @parameter_name = "Return Type"
end

/*	Get Object ID */
select @procedure_id = object_id(@full_procedure_name)

if ((charindex('%',@full_procedure_name) = 0) and
	(charindex('_',@full_procedure_name) = 0)  and
	@procedure_id != 0)
begin
/*
** this block is for the case where there is no pattern
** matching required for the procedure name
*/
	insert #oledb_results_table
	select	/* INTn, FLOATn, DATETIMEn and MONEYn types */
		PROCEDURE_CATALOG = db_name(),
		PROCEDURE_SCHEMA = user_name(o.uid),
		PROCEDURE_NAME = o.name,
                PARAMETER_NAME =
                        case
                            when c.name = 'Return Type' then 'RETURN_VALUE'
                            else c.name
                        end,
                ORDINAL_POSITION = convert(int,c.colid),
                PARAMETER_TYPE =
		case
			when c.name = 'Return Type'			
				then convert(smallint, 4) /*DBPARAMTYPE_RETURNVALUE*/
		when c.status2 = 1
			then convert(smallint, 1) /*DBPARAMTYPE_INPUT*/
		when c.status2 = 2
			then convert(smallint, 3) /*DBPARAMTYPE_OUTPUT*/
		when c.status2 = 4			     		
			then  convert(smallint, 2) /*DBPARAMTYPE_INPUTOUTPUT*/
		else null
		end,
		PARAMETER_HASDEFAULT = convert(bit,0),
		PARAMETER_DEFAULT = convert(varchar(32),null),
		IS_NULLABLE =	/* set nullability from status flag */
			convert(smallint, 1),

		DATA_TYPE = 0,
					   
		CHARACTER_MAXIMUM_LENGTH = isnull(convert(int, c.prec),
					isnull(d.data_precision, convert(int,c.length)))
					     +isnull(d.aux, convert(int,
							     ascii(substring("???AAAFFFCKFOLS",
								          2*(d.ss_dtype%35+1)
									  +2-8/c.length,1))
							  -60)),
		CHARACTER_OCTET_LENGTH = 
				/*
				** check if in the list
				** if so, return a 1 and multiply it by the precision
				** if not, return a 0 and multiply it by the precision
				*/
				convert(smallint,
				    substring('0111111',
					charindex(char(c.type),
					@char_bin_types)+1, 1)) *
				/* calculate the precision */
				isnull(convert(int, c.prec),
				    isnull(convert(int, d.data_precision),
					convert(int, c.length)))
				    +isnull(d.aux, convert(int,
					ascii(substring('???AAAFFFCKFOLS',
			   2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)),
		
		NUMERIC_PRECISION = d.data_precision,
		NUMERIC_PRECISION_RADIX = d.numeric_radix,
		NUMERIC_SCALE = isnull(convert(smallint, c.scale),
			convert(smallint, d.numeric_scale)) +
			convert(smallint, 
				isnull(d.aux, ascii(substring("<<<<<<<<<<<<<<?",
					        2*(d.ss_dtype%35+1)
						+2-8/c.length,
					        1))-60)),
				
		DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
		TYPE_NAME = rtrim(substring(d.type_name,
							1+isnull(d.aux,
							     ascii(substring("III<<<MMMI<<A<A",
									2*(d.ss_dtype%35+1)
									+2-8/c.length,
						        1)) - 60), 18)),
		LOCAL_TYPE_NAME = rtrim(substring(d.type_name,
									1+isnull(d.aux,
									     ascii(substring("III<<<MMMI<<A<A",
											2*(d.ss_dtype%35+1)
											+2-8/c.length,
						        1)) - 60), 18))
		
		
		
		
		
	from
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t,
		sysprocedures p
		
	where
		o.id = @procedure_id
		and c.id = o.id
		and c.type = d.ss_dtype
		and c.name like @parameter_name
		and d.ss_dtype in (111, 109, 38, 110)	/* Just *N types */
		and c.number = @group_num
		
	union
	select
		PROCEDURE_CATALOG = db_name(),
		PROCEDURE_SCHEMA = user_name(o.uid),
		PROCEDURE_NAME = o.name,
		PARAMETER_NAME = 'RETURN_VALUE',
		ORDINAL_POSITION = convert(tinyint, 0),
		PARAMETER_TYPE = convert(smallint, 4), /* return parameter */
		PARAMETER_HASDEFAULT = convert(bit,0),
		PARAMETER_DEFAULT = convert(varchar(32),null),
		IS_NULLABLE = convert(smallint, 1),
		DATA_TYPE =  0,
		CHARACTER_MAXIMUM_LENGTH = isnull(d.data_precision, convert(int,d.length))
					     +isnull(d.aux, convert(int,
							     ascii(substring("???AAAFFFCKFOLS",
								          2*(d.ss_dtype%35+1)
									  +2-8/d.length,1))
							  -60)),
		CHARACTER_OCTET_LENGTH = NULL,
		NUMERIC_PRECISION = d.data_precision,
		NUMERIC_PRECISION_RADIX = d.numeric_radix,
		NUMERIC_SCALE = d.numeric_scale +convert(smallint,
					   isnull(d.aux,
					     ascii(substring("<<<<<<<<<<<<<<?",
						        2*(d.ss_dtype%35+1)
							+2-8/d.length,
						        1))-60)),
				
		DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
		TYPE_NAME = d.type_name,
		LOCAL_TYPE_NAME = d.type_name
		
             
               
               
                
                
	from
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t
		
	where
		o.id = @procedure_id
		and d.ss_dtype = 56  /* int for return code */
		and t.type = 56
		and o.type = 'P'
		and (@parameter_name = '%' or @parameter_name = 'RETURN_VALUE')
		
	union
	select		   /* All other types including user data types */
		PROCEDURE_CATALOG = db_name(),
		PROCEDURE_SCHEMA = user_name(o.uid),
		PROCEDURE_NAME = o.name,
		PARAMETER_NAME = 	
			case	
		 	    when c.name = 'Return Type' then 'RETURN_VALUE'
			    else c.name
			end,
		ORDINAL_POSITION = convert(int,c.colid),
		PARAMETER_TYPE = 
		case
			when c.name = 'Return Type'			
				then convert(smallint, 4) /*DBPARAMTYPE_RETURNVALUE*/
		when c.status2 = 1
			then convert(smallint, 1) /*DBPARAMTYPE_INPUT*/
		when c.status2 = 2
			then convert(smallint, 3) /*DBPARAMTYPE_OUTPUT*/
		when c.status2 = 4			     		
			then  convert(smallint, 2) /*DBPARAMTYPE_INPUTOUTPUT*/
		else null
		end,		
		PARAMETER_HASDEFAULT = convert(bit,0),
		PARAMETER_DEFAULT = convert(varchar(32),null),
		IS_NULLABLE = convert(smallint, 1),
		DATA_TYPE = 0,
		CHARACTER_MAXIMUM_LENGTH = 
				case 
					when d.data_precision = 0
					then convert(int,0)
				else		
				isnull(convert(int, c.prec),
					isnull(d.data_precision, convert(int,c.length)))
					     +isnull(d.aux, convert(int,
							     ascii(substring("???AAAFFFCKFOLS",
								       2*(d.ss_dtype%35+1)
								       +2-8/c.length,1))
								       -60))
		end,
		CHARACTER_OCTET_LENGTH = 
				/*
				** check if in the list
				** if so, return a 1 and multiply it by the precision
				** if not, return a 0 and multiply it by the precision
				*/
				convert(smallint,
				    substring('0111111',
					charindex(char(c.type),
					@char_bin_types)+1, 1)) *
				/* calculate the precision */
				isnull(convert(int, c.prec),
				    isnull(convert(int, d.data_precision),
					convert(int, c.length)))
				    +isnull(d.aux, convert(int,
					ascii(substring('???AAAFFFCKFOLS',
			   2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)),
			
		
		NUMERIC_PRECISION = d.data_precision,
		NUMERIC_PRECISION_RADIX = d.numeric_radix,
		NUMERIC_SCALE = isnull(convert(smallint, c.scale),
			convert(smallint, d.numeric_scale))
			+ convert(smallint,
					  isnull(d.aux,
					    ascii(substring("<<<<<<<<<<<<<<?",
							    2*(d.ss_dtype%35+1)
							    +2-8/c.length,
							    1))-60)),
		
		/* set nullability from status flag */
		
		DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
		TYPE_NAME = 
					case 
					    when t.name = 'extended type' 
						then isnull(get_xtypename(c.xtype, c.xdbid), 
											t.name)
					    when t.type = 58
					    	then "smalldatetime"
					    when t.usertype in (44,45,46)
					    	then "unsigned "+substring(t.name,
					    			charindex("u",t.name) + 1,
					    			charindex("t",t.name))
					    else 
						t.name
			end,
		LOCAL_TYPE_NAME = case when t.name = 'extended type' 
				then isnull(get_xtypename(c.xtype, c.xdbid), t.name)
				when t.type = 58  	then "smalldatetime"
			        when t.usertype in (44,45,46)
				then "unsigned "+substring(t.name,
					charindex("u",t.name) + 1,
					charindex("t",t.name))
				 else t.name
			end
               
               
               
                
	from
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t
		
	where
		o.id = @procedure_id
		and c.id = o.id
		and c.type *= d.ss_dtype
		and c.usertype *= t.usertype
		and c.name like @parameter_name
		and c.number = @group_num
		and d.ss_dtype not in (111, 109, 38, 110) /* No *N types */
		

	 order by convert(int,colid)
end
else
begin
	/* 
	** this block is for the case where there IS pattern
	** matching done on the table name
	*/
	if @procedure_schema is null
		select @procedure_schema= '%'
	 insert #oledb_results_table
	select	/* INTn, FLOATn, DATETIMEn and MONEYn types */
		PROCEDURE_CATALOG = db_name(),
		PROCEDURE_SCHEMA = user_name(o.uid),
		PROCEDURE_NAME = o.name,
                PARAMETER_NAME =
                        case
                            when c.name = 'Return Type' then 'RETURN_VALUE'
                            else c.name
                        end,
                ORDINAL_POSITION = convert(int,c.colid),
                PARAMETER_TYPE =
		case
			when c.name = 'Return Type'			
				then convert(smallint, 4) /*DBPARAMTYPE_RETURNVALUE*/
		when c.status2 = 1
			then convert(smallint, 1) /*DBPARAMTYPE_INPUT*/
		when c.status2 = 2
			then convert(smallint, 3) /*DBPARAMTYPE_OUTPUT*/
		when c.status2 = 4			     		
			then  convert(smallint, 2) /*DBPARAMTYPE_INPUTOUTPUT*/
		else null
		end,
		PARAMETER_HASDEFAULT = convert(bit,0),
		PARAMETER_DEFAULT = convert(varchar(32),null),
		IS_NULLABLE = convert(smallint, 1),
		DATA_TYPE = 0,
		CHARACTER_MAXIMUM_LENGTH = isnull(convert(int, c.prec), 
						isnull(d.data_precision, convert(int,c.length)))
					     +isnull(d.aux, convert(int,
							     ascii(substring("???AAAFFFCKFOLS",
								           2*(d.ss_dtype%35+1)
									   +2-8/c.length,1))
						           -60)),
		CHARACTER_OCTET_LENGTH = 
				/*
				** check if in the list
				** if so, return a 1 and multiply it by the precision
				** if not, return a 0 and multiply it by the precision
				*/
				convert(smallint,
				    substring('0111111',
					charindex(char(c.type),
					@char_bin_types)+1, 1)) *
				/* calculate the precision */
				isnull(convert(int, c.prec),
				    isnull(convert(int, d.data_precision),
					convert(int, c.length)))
				    +isnull(d.aux, convert(int,
					ascii(substring('???AAAFFFCKFOLS',
			   2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)),
		
		
		NUMERIC_PRECISION = d.data_precision,
		NUMERIC_PRECISION_RADIX = d.numeric_radix,
		NUMERIC_SCALE = isnull(convert(smallint, c.scale),
				convert(smallint, d.numeric_scale))
				+ convert(smallint,
					    isnull(d.aux,
					     ascii(substring("<<<<<<<<<<<<<<?",
							   2*(d.ss_dtype%35+1)
							   +2-8/c.length,
							   1))-60)),
		
		/* set nullability from status flag */
		
		DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
		TYPE_NAME = rtrim(substring(d.type_name,
						    1+isnull(d.aux,
							     ascii(substring("III<<<MMMI<<A<A",
							                  2*(d.ss_dtype%35+1)
									  +2-8/c.length,
						          1))-60), 18)),
		LOCAL_TYPE_NAME = rtrim(substring(d.type_name,
								    1+isnull(d.aux,
									     ascii(substring("III<<<MMMI<<A<A",
									                  2*(d.ss_dtype%35+1)
											  +2-8/c.length,
						          1))-60), 18))
                
		
               
                
	from
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t
		
	where
		o.name like @procedure_name
		and user_name(o.uid) like @procedure_schema
		and o.id = c.id
		and c.type = d.ss_dtype
		and c.name like @parameter_name
		
		/* Just procs & sqlj procs and funcs */
		and o.type in ('P', 'F')
		and d.ss_dtype in (111, 109, 38, 110)	/* Just *N types */
	union
		select distinct
			PROCEDURE_CATALOG = db_name(),
			PROCEDURE_SCHEMA = user_name(o.uid),
			PROCEDURE_NAME = o.name,
			PARAMETER_NAME = 'RETURN_VALUE',
			ORDINAL_POSITION = convert(tinyint, 0),
			PARAMETER_TYPE = convert(smallint, 4), /* return parameter */
			PARAMETER_HASDEFAULT = convert(bit,0),
			PARAMETER_DEFAULT = convert(varchar(32),null),
			IS_NULLABLE = convert(smallint, 1),
			DATA_TYPE = 0,
			CHARACTER_MAXIMUM_LENGTH = isnull(d.data_precision, convert(int,d.length))
						     +isnull(d.aux, convert(int,
								     ascii(substring("???AAAFFFCKFOLS",
									          2*(d.ss_dtype%35+1)
										  +2-8/d.length,1))
								  -60)),
			CHARACTER_OCTET_LENGTH = NULL,
			
			NUMERIC_PRECISION = d.data_precision,
			NUMERIC_PRECISION_RADIX = d.numeric_radix,
			NUMERIC_SCALE = d.numeric_scale +convert(smallint,
						   isnull(d.aux,
						     ascii(substring("<<<<<<<<<<<<<<?",
							        2*(d.ss_dtype%35+1)
								+2-8/d.length,
							        1))-60)),
			
			
			DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
			TYPE_NAME = d.type_name,
			LOCAL_TYPE_NAME = d.type_name
			
	                     
	                
		from
			sysobjects o,
			sybsystemprocs.dbo.spt_datatype_info d,
			systypes t,
			sysprocedures p
			
		where
			o.name like @procedure_name
			and user_name(o.uid) like @procedure_schema
			and d.ss_dtype = 56  /* int for return code */
			and t.type = 56
			and o.type = 'P'			/* Just Procedures */
			and p.id = o.id
		and 'RETURN_VALUE' like @parameter_name
		union
			select		   /* All other types including user data types */
				PROCEDURE_CATALOG = db_name(),
				PROCEDURE_SCHEMA = user_name(o.uid),
				PROCEDURE_NAME = o.name,
		                PARAMETER_NAME =
				case
					when c.name = 'Return Type' then 'RETURN_VALUE'
					else c.name
				end,
				ORDINAL_POSITION = convert(int,c.colid),
		                PARAMETER_TYPE =
				case
					when c.name = 'Return Type'
						then convert(smallint, 4)
				when c.status2 = 1
					then convert(smallint, 1)
				when c.status2 = 2
					then convert(smallint, 3)
				when c.status2 = 4			     		
					then  convert(smallint, 2)                                        
					else null
				end,
				PARAMETER_HASDEFAULT = convert(bit,0),
				PARAMETER_DEFAULT = convert(varchar(32),null),
				IS_NULLABLE = convert(smallint, 1),
				DATA_TYPE =  0,
				CHARACTER_MAXIMUM_LENGTH = 
						case 
							when d.data_precision = 0
							then convert(int,0)
						else
						isnull(convert(int, c.prec),
							isnull(d.data_precision, convert(int,c.length)))
							     +isnull(d.aux, 
								     convert(int,
									     ascii(substring("???AAAFFFCKFOLS",
									                   2*(d.ss_dtype%35+1)
											   +2-8/c.length,1))
									           -60))
				end,
				CHARACTER_OCTET_LENGTH = 
						/*
						** check if in the list
						** if so, return a 1 and multiply it by the precision
						** if not, return a 0 and multiply it by the precision
						*/
						convert(smallint,
						    substring('0111111',
							charindex(char(c.type),
							@char_bin_types)+1, 1)) *
						/* calculate the precision */
						isnull(convert(int, c.prec),
						    isnull(convert(int, d.data_precision),
							convert(int, c.length)))
						    +isnull(d.aux, convert(int,
							ascii(substring('???AAAFFFCKFOLS',
					   2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)),
				
						
				NUMERIC_PRECISION = d.data_precision,
				NUMERIC_PRECISION_RADIX = d.numeric_radix,
				NUMERIC_SCALE = isnull(convert(smallint, c.scale),
					convert(smallint, d.numeric_scale))
					+ convert(smallint,
						 isnull(d.aux,
							ascii(substring("<<<<<<<<<<<<<<?",
							                2*(d.ss_dtype%35+1)
									+2-8/c.length,
							                1))-60)),
				
				/* set nullability from status flag */
				
				DESCRIPTION = convert(varchar(254),null),	/* Remarks are NULL */
				TYPE_NAME =
					case 
					    when t.name = 'extended type' 
						then isnull(get_xtypename(c.xtype, c.xdbid),
											t.name)
					    when t.type = 58
						then "smalldatetime"
					    when t.usertype in (44,45,46)
						then "unsigned "+substring(t.name,
						charindex("u",t.name) + 1,
						charindex("t",t.name))
					    else 
						t.name
		                        end,
		              	LOCAL_TYPE_NAME =
					case 
					    when t.name = 'extended type' 
						then isnull(get_xtypename(c.xtype, c.xdbid),
											t.name)
					    when t.type = 58
						then "smalldatetime"
					    when t.usertype in (44,45,46)
						then "unsigned "+substring(t.name,
						charindex("u",t.name) + 1,
						charindex("t",t.name))
					    else 
						t.name
		                        end
		              
		                
			from
				syscolumns c,
				sysobjects o,
				sybsystemprocs.dbo.spt_datatype_info d,
				systypes t
				
			where
				o.name like @procedure_name
				and user_name(o.uid) like @procedure_schema
				and o.id = c.id
				and c.type *= d.ss_dtype
				and c.usertype *= t.usertype
		
		                /* Just procs & sqlj procs and funcs */
				and o.type in ('P', 'F')
				and c.name like @parameter_name
				and d.ss_dtype not in (111, 109, 38, 110) /* No *N types */
				
		
	order by PROCEDURE_SCHEMA, PROCEDURE_NAME, convert(int,colid)
end


update #oledb_results_table set o.DATA_TYPE = m.data_type   from
	sybsystemprocs.dbo.spt_sybdrv m, #oledb_results_table  o
	where o.TYPE_NAME =  m.type_name 
if(@is_ado = 2)
begin
select * from #oledb_results_table order by PROCEDURE_CATALOG, PROCEDURE_SCHEMA, PROCEDURE_NAME
end
else if(@is_ado = 1)
begin
select 
PROCEDURE_CATALOG ,
  	PROCEDURE_SCHEMA ,
  	PROCEDURE_NAME  ,
  	PARAMETER_NAME  ,
  	ORDINAL_POSITION ,
  	PARAMETER_TYPE 		,
  	PARAMETER_HASDEFAULT	,
  	PARAMETER_DEFAULT	,
  	IS_NULLABLE		,
  	DATA_TYPE		,
  	CHARACTER_MAXIMUM_LENGTH	,
  	CHARACTER_OCTET_LENGTH		,
  	NUMERIC_PRECISION		,
  	NUMERIC_SCALE			,
  	DESCRIPTION		,
  	TYPE_NAME		,
  	LOCAL_TYPE_NAME	
    from #oledb_results_table order by PROCEDURE_CATALOG, PROCEDURE_SCHEMA, PROCEDURE_NAME
  	
end
drop table #oledb_results_table
if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx  

go
exec sp_procxmode 'sp_oledb_getprocedurecolumns', 'anymode'
go
grant execute on sp_oledb_getprocedurecolumns to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go


/*---------INDEXES-----------------------------------------------------------*/

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_getindexinfo')
begin
	drop procedure sp_oledb_getindexinfo
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */

/*
** Messages for "sp_oledb_getindexinfo"          18039
**
** 18039, "Table qualifier must be name of current database."
** 18040, "Catalog procedure '%1!' can not be run in a transaction.
**
*/

create procedure sp_oledb_getindexinfo (
	@table_name		varchar(96) = null,
	@table_owner		varchar(32) = null,
	@table_qualifier	varchar(32) = null,
	@index_name		varchar(96) = '%',
	@type			smallint = null)
as
declare @indid			int
declare @lastindid		int
declare @full_table_name	varchar(193/*70*/)
declare @startedInTransaction bit
declare @cmd	varchar(5000)

if (@@trancount > 0)
   select @startedInTransaction = 1
else
   select @startedInTransaction = 0

if @@trancount = 0
begin
	set chained off
end

set transaction isolation level 1
if (@startedInTransaction = 1)
 save transaction oledb_keep_temptable_tx    

if @table_name is null
begin
	select @cmd = "select
		TABLE_CATALOG=db_name(),					-- table_catalog
		TABLE_SCHEMA=user_name(o.uid),					-- table_schema
		TABLE_NAME=o.name,						-- table_name
		INDEX_CATALOG=db_name(),					-- index_catalog
		INDEX_SCHEMA=o.name,						-- index_schema
		INDEX_NAME=x.name,						-- index_name
		PRIMARY_KEY=	case 
				when x.status&2048 = 2048 then convert(bit,1)
				else
				convert(bit,0)
				end,						-- primary_key
		'UNIQUE' =	case 
				when x.status&2 != 2 then convert(bit,0)
				else
				    convert(bit,1)
				end,						-- uniqueval
		'CLUSTERED' =	case
				when x.status2&512 = 512 then convert(bit,1)
				else
				convert(bit,1)
				end,						-- clusteredval
		TYPE=		case 
				when x.indid > 1 then convert(smallint,4)
				when x.status2&512 = 512 then convert(smallint,1)
				else
				convert(smallint,1)
				end,						-- type
		FILL_FACTOR=x.fill_factor,					-- fill_factor
		INITIAL_SIZE=null,						-- initial_size
		NULLS=4,							-- nulls
		SORT_BOOKMARKS=convert(bit,0),						-- sort_bookmarks
		AUTO_UPDATE=convert(bit,1),					-- auto_update
		NULL_COLLATION=4,						-- null_collation
		ORDINAL_POSITION=convert(int,colid),						-- ordinal_position
		COLUMN_NAME=INDEX_COL(o.name,indid,colid),			-- column name
		COLUMN_GUID = convert(varchar(36), null),
		COLUMN_PROPID=null,						-- column propid
		COLLATION=1,							-- collation
		CARDINALITY=case
			when x.indid > 1 then NULL
			else
		rowcnt(x.doampg)			
--		row_count(db_id(), x.id)
			end, 							-- cardinality
		PAGES=case 
			when x.indid > 1 then NULL
			else
		data_pgs(x.id,doampg)			
-- 		data_pages(db_id(), x.id,
--		case
--			when x.indid = 1
--			then 0
--			else x.indid
--		end)
 			end,							-- pages
       		FILTER_CONDITION=null,						-- filter condition
       		INTEGRATED=convert(bit,1)					-- integrated
	from sysindexes x, syscolumns c, sysobjects o
	where   x.id = o.id
		and x.id = c.id
		and c.colid < keycnt+(x.status&16)/16
		and o.type = 'U' and x.id = object_id(o.name)
		and x.indid < 255
		and o.name like @table_name
		and (x.name like @index_name or x.name is null)"   
end
else
begin
	/*
	** Fully qualify table name.
	*/
	if @table_owner is null
	begin	/* If unqualified table name */
		select @full_table_name = @table_name
		select @table_owner = '%'
	end
	else
	begin	/* Qualified table name */
		select @full_table_name = @table_owner + '.' + @table_name
	end

	if @table_qualifier is null
		select @table_qualifier = '%'

	select @cmd = "select
		TABLE_CATALOG=db_name(),					-- table_catalog
		TABLE_SCHEMA=user_name(o.uid),					-- table_schema
		TABLE_NAME=o.name,						-- table_name
		INDEX_CATALOG=db_name(),					-- index_catalog
		INDEX_SCHEMA=o.name,						-- index_schema
		INDEX_NAME=x.name,						-- index_name
		PRIMARY_KEY=	case 
				when x.status&2048 = 2048 then convert(bit,1)
				else
				convert(bit,0)
				end,						-- primary_key
		'UNIQUE' =	case 
				when x.status&2 != 2 then convert(bit,0)
				else
				    convert(bit,1)
				end,						-- uniqueval
		'CLUSTERED' =	case
				when x.status2&512 = 512 then convert(bit,1)
				else
				convert(bit,1)
				end,						-- clusteredval
		TYPE=		case 
				when x.indid > 1 then 4
				when x.status2&512 = 512 then 1
				else
				1
				end,						-- type
		FILL_FACTOR=x.fill_factor,					-- fill_factor
		INITIAL_SIZE=null,						-- initial_size
		NULLS=4,							-- nulls
		SORT_BOOKMARKS=convert(bit,0),						-- sort_bookmarks
		AUTO_UPDATE=convert(bit,1),					-- auto_update
		NULL_COLLATION=4,						-- null_collation
		ORDINAL_POSITION=convert(int,colid),						-- ordinal_position
		COLUMN_NAME=INDEX_COL(@full_table_name,indid,colid),		-- column name
		COLUMN_GUID = convert(varchar(36), null),
		COLUMN_PROPID=null,						-- column propid
		COLLATION=case 
			when index_colorder(@full_table_name,indid,colid) = 'ASC' then convert(smallint, 1)
			when index_colorder(@full_table_name,indid,colid) = 'DESC' then convert(smallint, 2)
			else convert(smallint, 0)
			end,							-- collation
		CARDINALITY=case
			when x.indid > 1 then NULL
			else
		rowcnt(x.doampg)			
--		row_count(db_id(), x.id)
			end, 							-- cardinality
		PAGES=case 
			when x.indid > 1 then NULL
			else
		data_pgs(x.id,doampg)			
-- 		data_pages(db_id(), x.id,
--		case
 --			when x.indid = 1
 --			then 0
 --			else x.indid
 --			end)
 			end,							-- pages
       		FILTER_CONDITION=null,						-- filter condition
       		INTEGRATED=convert(bit,1)					-- integrated
	from sysindexes x, syscolumns c, sysobjects o
	where x.id = object_id(@full_table_name)
		and x.id = o.id
		and x.id = c.id
		and c.colid < keycnt+(x.status&16)/16
		and (x.indid > 0 and x.indid < 255)
		and db_name() like @table_qualifier
		and user_name(o.uid) like @table_owner
		and (x.name like @index_name or x.name is null)"
	
end /*for the @table_name is not null*/

if @type is not null
	select @cmd=@cmd+" and @type in (1,4)"
select @cmd = @cmd+" order by TABLE_CATALOG, TABLE_SCHEMA, INDEX_NAME, TYPE, TABLE_NAME"
execute(@cmd)

if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx    

return (0)
go
exec sp_procxmode 'sp_oledb_getindexinfo', 'anymode'
go
grant execute on sp_oledb_getindexinfo to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go



/*--------------------Primary Key-----------------------------*/
if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_primarykey')
begin
	drop procedure sp_oledb_primarykey
end
go


/* Sccsid = "%Z% generic/sproc/src/%M% %I% %G%" */
/*
** note: there is one raiserror message: 18040
**
** messages for "sp_oledb_primarykey"               18039, 18040
**
** 17461, "Object does not exist in this database."
** 18039, "table qualifier must be name of current database."
** 18040, "catalog procedure %1! can not be run in a transaction.", sp_oledb_primarykey
**
*/

create procedure sp_oledb_primarykey
			   @table_name		varchar(96) = null,
			   @table_owner 	varchar(32) = null,
			   @table_qualifier varchar(32) = null 
as
declare @keycnt smallint
declare @indexid smallint
declare @indexname varchar(96)
declare @i int
declare @id int
declare @uid int
select @id = NULL
declare @startedInTransaction bit

    if (@@trancount > 0)
  	select @startedInTransaction = 1
    else
        select @startedInTransaction = 0

	set nocount on

	if (@@trancount = 0)
	begin
		set chained off
	end

	set transaction isolation level 1

    if (@startedInTransaction = 1)
	save transaction oledb_keep_temptable_tx    

	if @table_owner is null
	begin
		select @id = id , @uid = uid
		from sysobjects 
		where name = @table_name
			and uid = user_id()
		if (@id is null)
		begin
			select @id = id ,@uid = uid
			from sysobjects 
			where name = @table_name
			and uid = 1
		end
	end
	else
	begin
		select @id = id , @uid = uid
		from sysobjects 
		where name = @table_name and uid = user_id(@table_owner)
	end

    select 
	TABLE_CATALOG=db_name(),
	TABLE_SCHEMA=user_name(@uid),
	TABLE_NAME=@table_name,
	COLUMN_NAME=index_col(@table_name, i.indid, c.colid, @uid),
	COLUMN_GUID=convert(varchar(30),null),
	COLUMN_PROPID=convert(int,null),
	ORDINAL=c.colid,
	PK_NAME=i.name
	from sysobjects o,sysindexes i,syscolumns c 
	where o.id=i.id 
		and c.id=i.id 
		and i.id=@id 
		and o.uid= @uid
		and i.indid > 0 
		and i.status2 & 2 = 2 
		and i.status & 2048 = 2048
		and index_col(@table_name, i.indid, c.colid, @uid) != null
	order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME
	
if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx    
	
return (0)
go
exec sp_procxmode 'sp_oledb_primarykey', 'anymode'
go
grant execute on sp_oledb_primarykey to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go

/*-----------------------Foreign Key----------------------------*/
if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_fkeys')
begin
	drop procedure sp_oledb_fkeys
end
go


/* sccsid = "%Z% generic/sproc/src/%M% %I% %G%" */
/*
** note: there is one raiserror message: 18040
**
** messages for "sp_oledb_fkeys"               18039, 18040
**
** 17461, "Object does not exist in this database." 
** 18040, "Catalog procedure %1! can not be run in a transaction.", sp_oledb_fkeys
** 18043 " Primary key table name or foreign key table name or both must be 
** given"
** 18044, "%1! table qualifier must be name of current database." [Primary
** key | Foreign key]
**
*/
CREATE PROCEDURE sp_oledb_fkeys
			   @pktable_name		varchar(32) = null,
			   @pktable_owner		varchar(32) = null,
			   @pktable_qualifier	varchar(32) = null,
			   @fktable_name		varchar(32) = null,
			   @fktable_owner		varchar(32) = null,
			   @fktable_qualifier	varchar(32) = null 
as
declare @ftabid int, @ptabid int, @constrid int,@keycnt int, @i int
declare @fokey1 int,  @fokey2 int,  @fokey3 int,  @fokey4 int,  @fokey5  int
declare @fokey6 int,  @fokey7 int,  @fokey8 int,  @fokey9 int,  @fokey10 int
declare @fokey11 int, @fokey12 int, @fokey13 int, @fokey14 int, @fokey15 int
declare @refkey1 int, @refkey2 int, @refkey3 int, @refkey4 int, @refkey5  int
declare @refkey6 int, @refkey7 int, @refkey8 int, @refkey9 int, @refkey10 int
declare @refkey11 int, @refkey12 int, @refkey13 int, @refkey14 int
declare @refkey15 int, @refkey16 int, @fokey16 int, @status int
declare @msg varchar(1024)
declare @msg2 varchar(1024)
declare @ordpkey	int
declare @notDeferrable smallint
declare @startedInTransaction bit

	if (@@trancount > 0)
	    select @startedInTransaction = 1
	else
	    select @startedInTransaction = 0


	set nocount on

	if (@@trancount = 0)
	begin
		set chained off
	end


	set transaction isolation level 1
	if (@startedInTransaction = 1)
	    save transaction oledb_keep_temptable_tx    

	select @notDeferrable = 3

	create table #ofkey_res( PK_TABLE_CATALOG varchar(32),
				PK_TABLE_SCHEMA       varchar(32),
				PK_TABLE_NAME       varchar(32),
				PK_COLUMN_NAME      varchar(32),
				PK_COLUMN_PROP_ID   int null,
				FK_TABLE_CATALOG varchar(32),
				FK_TABLE_SCHEMA       varchar(32),
				FK_TABLE_NAME       varchar(32),
				FK_COLUMN_NAME       varchar(32),
				FK_COLUMN_PROP_ID   int null,
				ORDINAL           int,	
				UPDATE_RULE varchar(32), 
				DELETE_RULE varchar(32),
				FK_NAME	varchar(32),
				PK_NAME	varchar(32),
				DEFERRABILITY smallint)
				
	if (@pktable_name is null) and (@fktable_name is null)
	begin	
		/* If neither primary key nor foreign key table names given */
		/* goto select directly and return no rows
		*/
		goto SelectClause
	end

	if @fktable_qualifier is not null
	begin
		if db_name() != @fktable_qualifier
		begin	
			goto SelectClause
		end
	end
	else
	begin
		/*
		** Now make sure that foreign table qualifier is pointing to the
		** current database in case it is not specified.
		*/
		select @fktable_qualifier = db_name()
	end

	if @pktable_qualifier is not null
	begin
		if db_name() != @pktable_qualifier
		begin	
			goto SelectClause
		end
	end
	else
	begin
		/*
		** Now make sure that primary table qualifier is pointing to the
		** current database in case it is not specified.
		*/
		select @pktable_qualifier = db_name()
	end


	create table #opid (pid int, uid int, name varchar(32))
	create table #ofid (fid int, uid int, name varchar(32))

	/* we will sort by fkey		*/
	/* unless pktable is null	*/

	select @ordpkey = 0

	if @pktable_name is not null
	begin 

		if (@pktable_owner is null)
		begin
			/* 
			** owner is NULL, so default to the current user
			** who owns this table, otherwise default to dbo
			** who owns this table.
			*/
			insert into #opid 
			select id, uid, name
			from sysobjects 
			where name = @pktable_name and uid = user_id()
			and type in ("S", "U")
			
			/* 
			** If the current user does not own the table, see
			** if the DBO of the current database owns the table.
			*/

			if ((select count(*) from #opid ) = 0)
			begin
				insert into #opid 
				select id, uid, name
				from sysobjects 
				where name = @pktable_name and uid = 1
				and type in ("S", "U")
			end
		end
		else
		begin
			insert into #opid
			select id, uid, name
			from sysobjects
			where name = @pktable_name 
			and uid = user_id(@pktable_owner)
			and type in ("S", "U")
		end
	end
	else 
	begin
		if (@pktable_owner is null)
		begin
		/* 
		** If neither pktable_name nor pktable_owner is specified,
		** then we are interested in every user or system table.
		*/
			insert into #opid 
			select id, uid, name
			from sysobjects 
			where type in ("S", "U")
		end
		else
		begin
			insert into #opid
			select id, uid, name
			from sysobjects
			where  uid = user_id(@pktable_owner)
			and type in ("S", "U")
		end
	end
		
	if @fktable_name is not null
	begin 
		/* sort by pkey	*/
		select @ordpkey = 1

		if (@fktable_owner is null)
		begin
			/* 
			** owner is NULL, so default to the current user
			** who owns this table, otherwise default to dbo
			** who owns this table.
			*/
			insert into #ofid 
			select id, uid, name
			from sysobjects 
			where name = @fktable_name and uid = user_id()
			and type in ("S", "U")

			/* 
			** If the current user does not own the table, see
			** if the DBO of the current database owns the table.
			*/

			if ((select count(*) from #opid ) = 0)
			begin
				insert into #ofid 
				select id, uid, name
				from sysobjects 
				where name = @fktable_name and uid = 1
				and type in ("S", "U")
			end
		end
		else
		begin
			insert into #ofid
			select id, uid, name
			from sysobjects
			where name = @fktable_name 
			and uid = user_id(@fktable_owner)
			and type in ("S", "U")
		end
	end
	else
	begin
		if (@fktable_owner is null)
		begin
		/* 
		** If neither fktable_name nor fktable_owner is specified,
		** then we are interested in every user table or systme 
		** table.
		*/			
			insert into #ofid 
			select id, uid, name
			from sysobjects 
			where type in ("S", "U")
		end
		else
		begin
			insert into #ofid
			select id, uid, name
			from sysobjects
			where  uid = user_id(@fktable_owner)
			and type in ("S", "U")
		end
	end

	if (((select count(*) from #ofid ) = 0) or
		((select count(*) from #opid) = 0))
	begin
		goto SelectClause
	end


	create table #opkeys(seq int,  keys varchar(32) null)
	create table #ofkeys(seq int, keys varchar(32) null)

	/*
	** Since there are possibly multiple rows in sysreferences
	** that describe foreign and primary key relationships among
	** two tables, so we declare a cursor on the selection from
	** sysreferences and process the output at row by row basis.
	*/
		
	declare curs_sysreferences cursor
	for
	select tableid, reftabid, constrid, keycnt,
	fokey1, fokey2, fokey3, fokey4, fokey5, fokey6, fokey7, fokey8, 
	fokey9, fokey10, fokey11, fokey12, fokey13, fokey14, fokey15,
	fokey16, refkey1, refkey2, refkey3, refkey4, refkey5,
	refkey6, refkey7, refkey8, refkey9, refkey10, refkey11,
	refkey12, refkey13, refkey14, refkey15, refkey16
	from sysreferences
	where tableid in (
		select fid from #ofid)
	and reftabid in (
		select pid from #opid)
	and frgndbname is NULL and pmrydbname is NULL
	for read only

	open  curs_sysreferences

	fetch  curs_sysreferences into @ftabid, @ptabid, @constrid, @keycnt,@fokey1, 
	@fokey2, @fokey3,  @fokey4, @fokey5, @fokey6, @fokey7, @fokey8, 
	@fokey9, @fokey10, @fokey11, @fokey12, @fokey13, @fokey14, @fokey15, 
	@fokey16, @refkey1, @refkey2, @refkey3, @refkey4, @refkey5, @refkey6, 
	@refkey7, @refkey8, @refkey9, @refkey10, @refkey11, @refkey12, 
	@refkey13, @refkey14, @refkey15, @refkey16

	while (@@sqlstatus = 0)
	begin
		/*
		** For each row of sysreferences which describes a foreign-
		** primary key relationship, do the following.
		*/

		/*
		** First store the column names that belong to primary keys
		** in table #pkeys for later retrieval.
		*/

		delete #opkeys
		insert #opkeys values(1, col_name(@ptabid,@refkey1))
		insert #opkeys values(2, col_name(@ptabid,@refkey2))
		insert #opkeys values(3, col_name(@ptabid,@refkey3))
		insert #opkeys values(4, col_name(@ptabid,@refkey4))
		insert #opkeys values(5, col_name(@ptabid,@refkey5))
		insert #opkeys values(6, col_name(@ptabid,@refkey6))
		insert #opkeys values(7, col_name(@ptabid,@refkey7))
		insert #opkeys values(8, col_name(@ptabid,@refkey8))
		insert #opkeys values(9, col_name(@ptabid,@refkey9))
		insert #opkeys values(10, col_name(@ptabid,@refkey10))
		insert #opkeys values(11, col_name(@ptabid,@refkey11))
		insert #opkeys values(12, col_name(@ptabid,@refkey12))
		insert #opkeys values(13, col_name(@ptabid,@refkey13))
		insert #opkeys values(14, col_name(@ptabid,@refkey14))
		insert #opkeys values(15, col_name(@ptabid,@refkey15))
		insert #opkeys values(16, col_name(@ptabid,@refkey16))
	
		/*
		** Second store the column names that belong to foreign keys
		** in table #fkeys for later retrieval.
		*/
		
		delete #ofkeys
		insert #ofkeys values(1, col_name(@ftabid,@fokey1))
		insert #ofkeys values(2, col_name(@ftabid,@fokey2))
		insert #ofkeys values(3, col_name(@ftabid,@fokey3))
		insert #ofkeys values(4, col_name(@ftabid,@fokey4))
		insert #ofkeys values(5, col_name(@ftabid,@fokey5))
		insert #ofkeys values(6, col_name(@ftabid,@fokey6))
		insert #ofkeys values(7, col_name(@ftabid,@fokey7))
		insert #ofkeys values(8, col_name(@ftabid,@fokey8))
		insert #ofkeys values(9, col_name(@ftabid,@fokey9))
		insert #ofkeys values(10, col_name(@ftabid,@fokey10))
		insert #ofkeys values(11, col_name(@ftabid,@fokey11))
		insert #ofkeys values(12, col_name(@ftabid,@fokey12))
		insert #ofkeys values(13, col_name(@ftabid,@fokey13))
		insert #ofkeys values(14, col_name(@ftabid,@fokey14))
		insert #ofkeys values(15, col_name(@ftabid,@fokey15))
		insert #ofkeys values(16, col_name(@ftabid,@fokey16))
	
		/* 
		** For each column of the current foreign-primary key relation,
		** create a row into result table: #fkey_res.
		*/

		select @i = 1
		while (@i <= @keycnt)
		begin
			insert into #ofkey_res 
				select @pktable_qualifier,
				(select user_name(uid) from #opid where pid = @ptabid),
				object_name(@ptabid),
				(select keys from #opkeys where seq = @i),
				null,
				@fktable_qualifier,
				(select user_name(uid) from #ofid where fid = @ftabid),
				object_name(@ftabid), 
				(select keys from #ofkeys where seq = @i), null, @i,
				"NO_ACTION", "NO_ACTION",
			/* Foreign Key */				
				object_name(@constrid),
			/* Primary key name */
		                (select name from sysindexes where id = @ptabid
		                    and status > 2048 and status < 32768),
		           @notDeferrable      
			select @i = @i + 1
		end
		
		/* 
		** Go to the next foreign-primary key relationship if any.
		*/

		fetch  curs_sysreferences into @ftabid, @ptabid, @constrid, @keycnt,@fokey1, 
		@fokey2, @fokey3,  @fokey4, @fokey5, @fokey6, @fokey7, @fokey8, 
		@fokey9, @fokey10, @fokey11, @fokey12, @fokey13, @fokey14, @fokey15, 
		@fokey16, @refkey1, @refkey2, @refkey3, @refkey4, @refkey5, @refkey6, 
		@refkey7, @refkey8, @refkey9, @refkey10, @refkey11, @refkey12, 
		@refkey13, @refkey14, @refkey15, @refkey16
	end

	close curs_sysreferences
	deallocate cursor curs_sysreferences

	/*
	** Everything is now in the result table #fkey_res, so go ahead
	** and select from the table now.
	*/

SelectClause:
	select PK_TABLE_CATALOG, 
		PK_TABLE_SCHEMA, 
		PK_TABLE_NAME,
		PK_COLUMN_NAME, 
		PK_COLUMN_GUID = convert(varchar(36), null),
		PK_COLUMN_PROP_ID = convert(int, null),
		FK_TABLE_CATALOG, 
		FK_TABLE_SCHEMA, 
		FK_TABLE_NAME, 
		FK_COLUMN_NAME, 
		FK_COLUMN_GUID = convert(varchar(36), null),
		FK_COLUMN_PROP_ID = convert(int, null),			
		ORDINAL, 
		UPDATE_RULE, 
		DELETE_RULE,
		FK_NAME,
		PK_NAME,
		DEFERRABILITY
	from #ofkey_res fkey
	order by FK_TABLE_CATALOG,FK_TABLE_SCHEMA, FK_TABLE_NAME

drop table #ofkey_res
drop table #opkeys
drop table #ofkeys

if (@startedInTransaction = 1)
   rollback transaction oledb_keep_temptable_tx    

go
exec sp_procxmode 'sp_oledb_fkeys', 'anymode'
go
grant execute on sp_oledb_fkeys to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go



/* --------------COLUMNS ------------------------------------- */

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_columns')
begin
	drop procedure sp_oledb_columns
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G% " */
/*      10.0        07/20/93        sproc/columns */
 

/* This is the version for servers which support UNION */

/* This routine is intended for support of oledb connectivity.  Under no
** circumstances should changes be made to this routine unless they are
** to fix oledb related problems.  All other users are at there own risk!
**
** Please be aware that any changes made to this file (or any other oledb
** support routine) will require Sybase to recertify the SQL server as
** oledb compliant.  This process is currently being managed internally
** by the "Interoperability Engineering Technology Solutions Group" here
** within Sybase.
*/

CREATE PROCEDURE sp_oledb_columns (
				 @table_name		varchar(96) = null,
				 @table_owner		varchar(32) = null,
				 @table_qualifier	varchar(32) = null,
				 @column_name		varchar(96) = null,
				 @is_ado            int =1 )
AS
    declare @full_table_name    varchar(193)
    declare @table_id int
    declare @char_bin_types   varchar(32)
    declare @startedInTransaction bit
    
    if (@@trancount > 0)
  	select @startedInTransaction = 1
    else
        select @startedInTransaction = 0    
        
    set transaction isolation level 1

    if (@startedInTransaction = 1)
	save transaction oledb_keep_temptable_tx      
 
/* character and binary datatypes */
	select @char_bin_types =
		char(47)+char(39)+char(45)+char(37)+char(35)+char(34)

    create table #results_table
		(TABLE_CATALOG  varchar(32) null,
		TABLE_SCHEMA varchar(32) null,
		TABLE_NAME   varchar(32) null,
		COLUMN_NAME  varchar(32) null,
		COLUMN_GUID  varchar(36)  null,
		COLUMN_PROPID int null,
		ORDINAL_POSITION int null,
		COLUMN_HASDEFAULT bit default 1,
		COLUMN_DEFAULT varchar(32) null,
		COLUMN_FLAGS int null,
		usertype int null,
        	IS_NULLABLE bit default 1,
		DATA_TYPE smallint null,
		TYPE_GUID varchar(36) null,
		CHARACTER_MAXIMUM_LENGTH int null,
            	CHARACTER_OCTET_LENGTH int null, 
		NUMERIC_PRECISION smallint null,
		NUMERIC_PRECISION_RADIX  smallint null,
		TYPE_NAME varchar (32) null,
		NUMERIC_SCALE smallint null,
		DATETIME_PRECISION int null,									
		CHARACTER_SET_CATALOG varchar(32) null,
		CHARACTER_SET_SCHEMA varchar(32) null,
		CHARACTER_SET_NAME varchar(32) null,
		COLLATION_CATALOG varchar(32) null,
		COLLATION_SCHEMA varchar(32) null,
		COLLATION_NAME varchar(32) null,
		DOMAIN_CATALOG varchar(32) null,
		DOMAIN_SCHEMA varchar(32) null,
		DOMAIN_NAME varchar(32) null,
		DESCRIPTION varchar(32) null,
		tds_type smallint null,
		id int null,
		col_len int null)
		
    if @column_name is null /*	If column name not supplied, match all */
	select @column_name = '%'

    /* Check if the current database is the same as the one provided */
    if @table_qualifier is not null
    begin
		if db_name() != @table_qualifier
		begin	/* 
			** If qualifier doesn't match current database 
			** force a no-row selection
			*/
			goto SelectClause
		end
    end

    if @table_name is null
    begin	/*	If table name not supplied, match all */
		select @table_name = '%'
    end

    if @table_owner is null
    begin	/* If unqualified table name */
		SELECT @full_table_name = @table_name
    end
    else
    begin	/* Qualified table name */
		SELECT @full_table_name = @table_owner + '.' + @table_name
    end

    /* Get Object ID */
    SELECT @table_id = object_id(@full_table_name)


    /* If the table name parameter is valid, get the information */ 
    if ((charindex('%',@full_table_name) = 0) and
		(charindex('_',@full_table_name) = 0)  and
		@table_id != 0)
    begin
    insert into #results_table
	SELECT	/* INTn, FLOATn, DATETIMEn and MONEYn types */
		TABLE_CATALOG = DB_NAME(),
		TABLE_SCHEMA = USER_NAME(o.uid),
		TABLE_NAME = o.name,
		COLUMN_NAME = c.name,
		COLUMN_GUID = convert(varchar(36), null),
		COLUMN_PROPID = convert(int, null),
		ORDINAL_POSITION = convert(int,c.colid),
		COLUMN_HASDEFAULT = convert(bit,c.cdefault&1),
		COLUMN_DEFAULT = convert(varchar(254), null),
		/*CT: IsNullable 0x20 (c.status&8 * 4),
		      MaybeNullable 0x40 (c.status&8 * 8) - always MaybeNullable if IsNullable
		      IsFixedLength 0x10 - yes
		      IsLong 0x80 - no
		*/
		COLUMN_FLAGS = convert(int, c.status&8) * 12 + 16,
		usertype = t.usertype,
                IS_NULLABLE = convert(bit, c.status&8),
		DATA_TYPE = convert(smallint, m.data_type) ,
		TYPE_GUID = convert(varchar(36), null),
		CHARACTER_MAXIMUM_LENGTH = convert(int, null),  /*CT: only for char, binary and bit in oledb */
            	CHARACTER_OCTET_LENGTH = convert(int, null), /*CT: only for char, binary and bit in oledb */
		/* CT: MONEYN, INTN, DATETIMN and FLOATN will all be included */
		NUMERIC_PRECISION = isnull(convert(smallint, c.prec),
			         isnull(convert(smallint, d.data_precision),
			        convert(smallint,c.length)))
				+isnull(d.aux, convert(smallint,
				ascii(substring("???AAAFFFCKFOLS",
				2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)),

		NUMERIC_PRECISION_RADIX = d.numeric_radix,
        	TYPE_NAME = 
		case 
			when convert(bit, (c.status & 0x80)) = 1 then 
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))+' identity'
		         else
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))
                 end,
		NUMERIC_SCALE = convert(smallint, null),  /*CT: only for decimal and numeric in oledb*/
		/*CT: in ss_dtype 111, 109, 38, 110, only 111 has non null value for this item
		  it is the datetime type scale, UI4 type*/
		DATETIME_PRECISION = case 
					when d.ss_dtype = 111  /*CT:  also the type is changed to UI4*/
						then 
						isnull(convert(int, c.scale), 
					        convert(int, d.numeric_scale))
						+convert(int,
						isnull(d.aux,
						ascii(substring("<<<<<<<<<<<<<<?",
						2*(d.ss_dtype%35+1)+2-8/c.length,
						1))-60))
					else
						convert(int, null)
				     end,					
				
		CHARACTER_SET_CATALOG = null,
		CHARACTER_SET_SCHEMA = null, 
		CHARACTER_SET_NAME = null,
		COLLATION_CATALOG = null,
		COLLATION_SCHEMA = null,
		COLLATION_NAME = null,
		DOMAIN_CATALOG = null,
		DOMAIN_SCHEMA = null,
		DOMAIN_NAME = null,
		DESCRIPTION = null,	/* Description are NULL */
		tds_type = c.type,
		id = c.id,
		col_len = c.length
		
	FROM
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t,
		sybsystemprocs.dbo.spt_sybdrv m
	WHERE
		o.id = @table_id
		AND c.id = o.id
		/*
		** We use syscolumn.usertype instead of syscolumn.type
		** to do join with systypes.usertype. This is because
		** for a column which allows null, type stores its
		** Server internal datatype whereas usertype still
		** stores its user defintion datatype.  For an example,
		** a column of type 'decimal NULL', its usertype = 26,
		** representing decimal whereas its type = 106 
		** representing decimaln. nullable in the select list
		** already tells user whether the column allows null.
		** In the case of user defining datatype, this makes
		** more sense for the user.
		*/
		AND c.usertype = t.usertype
		AND t.type = d.ss_dtype
		AND c.name like @column_name
	AND d.ss_dtype IN (111, 109, 38, 110)	/* Just *N types */
--	AND d.ss_dtype IN (111, 109, 38, 110, 68)	/* Just *N types */
		AND c.usertype < 100		/* No user defined types */
		AND m.type_name = 
		rtrim(substring(d.type_name,
		1+isnull(d.aux,
		ascii(substring("III<<<MMMI<<A<A",
		2*(d.ss_dtype%35+1)+2-8/c.length,
		1))-60), 18))
	UNION
	SELECT	/* All other types including user data types */
		TABLE_CATALOG = DB_NAME(),
		TABLE_SCHEMA = USER_NAME(o.uid),
		TABLE_NAME = o.name,
		COLUMN_NAME = c.name,
		COLUMN_GUID = convert(varchar(36), null),
		COLUMN_PROPID = convert(int, null),
		ORDINAL_POSITION = convert(int,c.colid),
		COLUMN_HASDEFAULT = convert(bit,c.cdefault&1),
		COLUMN_DEFAULT = convert(varchar(254), null),
		/*CT: IsNullable 0x20 (c.status&8 * 4),
		      MaybeNullable 0x40 (c.status&8 * 8) - always MaybeNullable if IsNullable
		      IsFixedLength 0x10 - yes
		      IsLong 0x80 - no
		*/
		COLUMN_FLAGS = convert(int, c.status&8) * 12 + 
			case when c.type in (37, 39, 155, 34, 35, 174) then 0
			else 16 end +
			case when c.type in (34, 35, 174) then 128 /*text, image and unitext type*/
			else 0 end,
		usertype = t.usertype,
                IS_NULLABLE = convert(bit, c.status&8),
		DATA_TYPE = convert(smallint, m.data_type) ,
		TYPE_GUID = convert(varchar(36), null),
		/*CT: only for char, binary and bit in oledb, see spec*/
		CHARACTER_MAXIMUM_LENGTH = case
						when c.type in (135, 155)
							then
								(isnull(convert(int, c.prec),
								 isnull(convert(int, d.data_precision),
								convert(int,c.length)))
								+isnull(d.aux, convert(int,
								ascii(substring("???AAAFFFCKFOLS",
								2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)))/2
						when c.type in (47, 39, 45, 37, 35, 34, 50)
							then 
								isnull(convert(int, c.prec),
								 isnull(convert(int, d.data_precision),
								convert(int,c.length)))
								+isnull(d.aux, convert(int,
								ascii(substring("???AAAFFFCKFOLS",
								2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
						else
							convert(int, null)
					   end,
												 
            	/*
            	** if the datatype is of type CHAR or BINARY
            	** then set char_octet_length to the same value
            	** assigned in the "prec" column.
            	**
            	** The first part of the logic is:
            	**
            	**   if(c.type is in (47, 39, 45, 37, 35, 34))
            	**       set char_octet_length = prec;
            	**   if (c.type is in (135, 155)
            	**       set char_octet_length = prec * 2;
            	**   else
            	**       set char_octet_length = null;
            	*/
           	CHARACTER_OCTET_LENGTH = case
					when c.type in (47, 39, 45, 37, 35, 34, 50)
						then /*same size as the CHARACTER_MAXIMUM_LENGTH */
							isnull(convert(int, c.prec),
							 isnull(convert(int, d.data_precision),
							convert(int,c.length)))
							+isnull(d.aux, convert(int,
							ascii(substring("???AAAFFFCKFOLS",
							2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
					when c.type in (135, 155)
						then /* CHARACTER_MAXIMUM_LENGTH * 2 for the unichar, univarchar*/
							isnull(convert(int, c.prec),
							 isnull(convert(int, d.data_precision),
							convert(int,c.length)))
							+isnull(d.aux, convert(int,
							ascii(substring("???AAAFFFCKFOLS",
							2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
					else
						convert(int, null)
				   end,
		NUMERIC_PRECISION = case 
					when c.type in (47, 39, 45, 37, 35, 34, 135, 155, 50, 58, 61, 123, 147,174)
						then convert(smallint,null)
					else
						isnull(convert(smallint, c.prec),
						 isnull(convert(smallint, d.data_precision),
						convert(smallint,c.length)))
						+isnull(d.aux, convert(smallint,
						ascii(substring("???AAAFFFCKFOLS",
						2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
					end,
		NUMERIC_PRECISION_RADIX = d.numeric_radix,				
		TYPE_NAME = 
		case 
			when convert(bit, (c.status & 0x80)) = 1 then 
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))+' identity'
		         else
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))
                 end,
		
      		NUMERIC_SCALE = case 
					when c.type in (63, 108, 55, 106) then
						isnull(convert(smallint, c.scale), 
					       convert(smallint, d.numeric_scale)) +
						convert(smallint, isnull(d.aux,
						ascii(substring("<<<<<<<<<<<<<<?",
						2*(d.ss_dtype%35+1)+2-8/c.length,
						1))-60))
					else
						convert(smallint, null)
				end,
		DATETIME_PRECISION = case 
					when c.type in (58, 61, 123, 147, 111) then 
						isnull(convert(int, c.scale), 
					        convert(int, d.numeric_scale)) +
						convert(int, isnull(d.aux,
						ascii(substring("<<<<<<<<<<<<<<?",
						2*(d.ss_dtype%35+1)+2-8/c.length,
						1))-60))
					else
						convert(int, null)
				     end,
		CHARACTER_SET_CATALOG = null,
		CHARACTER_SET_SCHEMA = null, 
		CHARACTER_SET_NAME = null,
		COLLATION_CATALOG = null,
		COLLATION_SCHEMA = null,
		COLLATION_NAME = null,
		DOMAIN_CATALOG = null,
		DOMAIN_SCHEMA = null,
		DOMAIN_NAME = null,
		DESCRIPTION = null,	/* Description are NULL */
		tds_type = c.type,
		id = c.id,
		col_len = c.length


	FROM
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t,
		sybsystemprocs.dbo.spt_sybdrv m
	WHERE
		o.id = @table_id
		AND c.id = o.id
		/*
		** We use syscolumn.usertype instead of syscolumn.type
		** to do join with systypes.usertype. This is because
		** for a column which allows null, type stores its
		** Server internal datatype whereas usertype still
		** stores its user defintion datatype.  For an example,
		** a column of type 'decimal NULL', its usertype = 26,
		** representing decimal whereas its type = 106 
		** representing decimaln. nullable in the select list
		** already tells user whether the column allows null.
		** In the case of user defining datatype, this makes
		** more sense for the user.
		*/
		AND c.usertype = t.usertype
		/*
		** We need a equality join with 
		** sybsystemprocs.dbo.spt_datatype_info here so that
		** there is only one qualified row returned from 
		** sybsystemprocs.dbo.spt_datatype_info, thus avoiding
		** duplicates.
		*/
		AND t.type = d.ss_dtype
		AND c.name like @column_name
	AND (d.ss_dtype NOT IN (111, 109, 38, 110) /* No *N types */
--	AND (d.ss_dtype NOT IN (111, 109, 38, 110, 68) /* No *N types */

			OR c.usertype >= 100) /* User defined types */
		AND m.type_name = 
		rtrim(substring(d.type_name,
		1+isnull(d.aux,
		ascii(substring("III<<<MMMI<<A<A",
		2*(d.ss_dtype%35+1)+2-8/c.length,
		1))-60), 18))

		ORDER BY TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME
	end
	else
    begin
	/* 
	** This block is for the case where there IS pattern
	** matching done on the table name. 
	*/
	if @table_owner is null /* If owner not supplied, match all */
			select @table_owner = '%'

	insert into #results_table
	SELECT	/* INTn, FLOATn, DATETIMEn and MONEYn types */
		TABLE_CATALOG = DB_NAME(),
		TABLE_SCHEMA = USER_NAME(o.uid),
		TABLE_NAME = o.name,
		COLUMN_NAME = c.name,
		COLUMN_GUID = convert(varchar(36), null),
		COLUMN_PROPID = convert(int, null),
		ORDINAL_POSITION = convert(int,c.colid),
		COLUMN_HASDEFAULT = convert(bit,c.cdefault&1),
		COLUMN_DEFAULT = convert(varchar(254), null),
		/*CT: IsNullable 0x20 (c.status&8 * 4),
		      MaybeNullable 0x40 (c.status&8 * 8) - always MaybeNullable if IsNullable
		      IsFixedLength 0x10 - yes
		      IsLong 0x80 - no
		*/
		COLUMN_FLAGS = convert(int, c.status&8) * 12 + 16,
		usertype = t.usertype,
                IS_NULLABLE = convert(bit, c.status&8),
		DATA_TYPE = convert(smallint, m.data_type) ,
		TYPE_GUID = convert(varchar(36), null),
		CHARACTER_MAXIMUM_LENGTH = convert(int, null),  /*CT: only for char, binary and bit in oledb */
            	CHARACTER_OCTET_LENGTH = convert(int, null), /*CT: only for char, binary and bit in oledb */
		/* CT: MONEYN, INTN, DATETIMN and FLOATN will all be included */
		NUMERIC_PRECISION = isnull(convert(smallint, c.prec),
			         isnull(convert(smallint, d.data_precision),
			        convert(smallint,c.length)))
				+isnull(d.aux, convert(smallint,
				ascii(substring("???AAAFFFCKFOLS",
				2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)),
        	NUMERIC_PRECISION_RADIX = d.num_prec_radix,
         	TYPE_NAME = 
		case 
			when convert(bit, (c.status & 0x80)) = 1 then 
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))+' identity'
		         else
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))
                 end,
		NUMERIC_SCALE = convert(smallint, null),  /*CT: only for decimal and numeric in oledb*/
		/*CT: in ss_dtype 111, 109, 38, 110, only 111 has non null value for this item
		  it is the datetime type scale, UI4 type*/
		DATETIME_PRECISION = case 
					when d.ss_dtype = 111  /*CT:  also the type is changed to UI4*/
						then 
						isnull(convert(int, c.scale), 
					        convert(int, d.numeric_scale))
						+convert(int,
						isnull(d.aux,
						ascii(substring("<<<<<<<<<<<<<<?",
						2*(d.ss_dtype%35+1)+2-8/c.length,
						1))-60))
					else
						convert(int, null)
				     end,
		CHARACTER_SET_CATALOG = null,
		CHARACTER_SET_SCHEMA = null, 
		CHARACTER_SET_NAME = null,
		COLLATION_CATALOG = null,
		COLLATION_SCHEMA = null,
		COLLATION_NAME = null,
		DOMAIN_CATALOG = null,
		DOMAIN_SCHEMA = null,
		DOMAIN_NAME = null,
		DESCRIPTION = null,	/* Description are NULL */
		tds_type = c.type,
		id = c.id,
		col_len = c.length

	FROM
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t,
		sybsystemprocs.dbo.spt_sybdrv m
	WHERE
		o.name like @table_name
		AND user_name(o.uid) like @table_owner
		AND o.id = c.id
		/*
		** We use syscolumn.usertype instead of syscolumn.type
		** to do join with systypes.usertype. This is because
		** for a column which allows null, type stores its
		** Server internal datatype whereas usertype still
		** stores its user defintion datatype.  For an example,
		** a column of type 'decimal NULL', its usertype = 26,
		** representing decimal whereas its type = 106 
		** representing decimaln. nullable in the select list
		** already tells user whether the column allows null.
		** In the case of user defining datatype, this makes
		** more sense for the user.
		*/
		AND c.usertype = t.usertype
		AND t.type = d.ss_dtype
		AND o.type != 'P'
		AND c.name like @column_name
	AND d.ss_dtype IN (111, 109, 38, 110)	/* Just *N types */
--	AND d.ss_dtype IN (111, 109, 38, 110, 68)	/* Just *N types */	
		AND c.usertype < 100
		AND m.type_name = 
		rtrim(substring(d.type_name,
		1+isnull(d.aux,
		ascii(substring("III<<<MMMI<<A<A",
		2*(d.ss_dtype%35+1)+2-8/c.length,
		1))-60), 18))
	UNION
	SELECT /* All other types including user data types */
		TABLE_CATALOG = DB_NAME(),
		TABLE_SCHEMA = USER_NAME(o.uid),
		TABLE_NAME = o.name,
		COLUMN_NAME= c.name,
		COLUMN_GUID = convert(varchar(36), null),
		COLUMN_PROPID = convert(int, null),
		ORDINAL_POSITION = convert(int,c.colid),
		COLUMN_HASDEFAULT = convert(bit,c.cdefault&1),
		COLUMN_DEFAULT = convert(varchar(254), null),
		/*CT: IsNullable 0x20 (c.status&8 * 4),
		      MaybeNullable 0x40 (c.status&8 * 8) - always MaybeNullable if IsNullable
		      IsFixedLength 0x10 - yes
		      IsLong 0x80 - no
		*/
		COLUMN_FLAGS = convert(int, c.status&8) * 12 + 
			case when c.type in (37, 39, 155, 34, 35, 174) then 0
			else 16 end +
			case when c.type in (34, 35, 174) then 128 /*text, image and unitext type*/
			else 0 end,
		usertype = t.usertype,
		IS_NULLABLE = convert(bit, c.status&8),
		DATA_TYPE = convert(smallint, m.data_type) ,
		TYPE_GUID = convert(varchar(36), null),
		/*CT: only for char, binary and bit in oledb, see spec*/
		CHARACTER_MAXIMUM_LENGTH = case
						when c.type in (135, 155)
							then
								(isnull(convert(int, c.prec),
								 isnull(convert(int, d.data_precision),
								convert(int,c.length)))
								+isnull(d.aux, convert(int,
								ascii(substring("???AAAFFFCKFOLS",
								2*(d.ss_dtype%35+1)+2-8/c.length,1))-60)))/2
						when c.type in (47, 39, 45, 37, 35, 34, 50)
							then 
								isnull(convert(int, c.prec),
								 isnull(convert(int, d.data_precision),
								convert(int,c.length)))
								+isnull(d.aux, convert(int,
								ascii(substring("???AAAFFFCKFOLS",
								2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
						else
							convert(int, null)
					   end,
												 
            	/*
            	** if the datatype is of type CHAR or BINARY
            	** then set char_octet_length to the same value
            	** assigned in the "prec" column.
            	**
            	** The first part of the logic is:
            	**
            	**   if(c.type in (47, 39, 45, 37, 35, 34))
            	**       set char_octet_length = prec;
            	**   if (c.type in (135, 155)
            	**       set char_octet_length = prec * 2;
            	**   else
            	**       set char_octet_length = null;
            	*/
           	CHARACTER_OCTET_LENGTH = case
					when c.type in (47, 39, 45, 37, 35, 34, 50)
						then /*same size as the CHARACTER_MAXIMUM_LENGTH */
							isnull(convert(int, c.prec),
							 isnull(convert(int, d.data_precision),
							convert(int,c.length)))
							+isnull(d.aux, convert(int,
							ascii(substring("???AAAFFFCKFOLS",
							2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
					when c.type in (135, 155)
						then /* CHARACTER_MAXIMUM_LENGTH * 2 for the unichar, univarchar*/
							isnull(convert(int, c.prec),
							 isnull(convert(int, d.data_precision),
							convert(int,c.length)))
							+isnull(d.aux, convert(int,
							ascii(substring("???AAAFFFCKFOLS",
							2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
					else
						convert(int, null)
				   end,
		NUMERIC_PRECISION = case 
					when c.type in (47, 39, 45, 37, 35, 34, 135, 155, 50, 58, 61, 123, 147,174)
						then convert(smallint,null)
					else
						isnull(convert(smallint, c.prec),
						 isnull(convert(smallint, d.data_precision),
						convert(smallint,c.length)))
						+isnull(d.aux, convert(smallint,
						ascii(substring("???AAAFFFCKFOLS",
						2*(d.ss_dtype%35+1)+2-8/c.length,1))-60))
					end,
		NUMERIC_PRECISION_RADIX = d.numeric_radix,
         	TYPE_NAME = 
		case 
			when convert(bit, (c.status & 0x80)) = 1 then 
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))+' identity'
		         else
				rtrim(substring(d.type_name,
		                        1+isnull(d.aux,
		                        ascii(substring('III<<<MMMI<<A<A',
		                        2*(d.ss_dtype%35+1)+2-8/c.length,
		                        1))-60), 18))
                 end,
		NUMERIC_SCALE = case 
					when c.type in (63, 108, 55, 106) then 
						isnull(convert(smallint, c.scale), 
					       convert(smallint, d.numeric_scale)) +
						convert(smallint, isnull(d.aux,
						ascii(substring("<<<<<<<<<<<<<<?",
						2*(d.ss_dtype%35+1)+2-8/c.length,
						1))-60))
					else
						convert(smallint, null)
				end,
		DATETIME_PRECISION = case 
					when c.type in (58, 61, 123, 147, 111) then
						isnull(convert(int, c.scale), 
					        convert(int, d.numeric_scale)) +
						convert(int, isnull(d.aux,
						ascii(substring("<<<<<<<<<<<<<<?",
						2*(d.ss_dtype%35+1)+2-8/c.length,
						1))-60))				
					else
						convert(int, null)
				     end,
		CHARACTER_SET_CATALOG = null,
		CHARACTER_SET_SCHEMA = null, 
		CHARACTER_SET_NAME = null,
		COLLATION_CATALOG = null,
		COLLATION_SCHEMA = null,
		COLLATION_NAME = null,
		DOMAIN_CATALOG = null,
		DOMAIN_SCHEMA = null,
		DOMAIN_NAME = null,
		DESCRIPTION  = null,
		tds_type = c.type,
		id = c.id,
		col_len = c.length

	FROM
		syscolumns c,
		sysobjects o,
		sybsystemprocs.dbo.spt_datatype_info d,
		systypes t,
		sybsystemprocs.dbo.spt_sybdrv m
	WHERE
		o.name like @table_name
		AND user_name(o.uid) like @table_owner
		AND o.id = c.id
		/*
		** We use syscolumn.usertype instead of syscolumn.type
		** to do join with systypes.usertype. This is because
		** for a column which allows null, type stores its
		** Server internal datatype whereas usertype still
		** stores its user defintion datatype.  For an example,
		** a column of type 'decimal NULL', its usertype = 26,
		** representing decimal whereas its type = 106 
		** representing decimaln. nullable in the select list
		** already tells user whether the column allows null.
		** In the case of user defining datatype, this makes
		** more sense for the user.
		*/
		AND c.usertype = t.usertype
		/*
		** We need a equality join with 
		** sybsystemprocs.dbo.spt_datatype_info here so that
		** there is only one qualified row returned from 
		** sybsystemprocs.dbo.spt_datatype_info, thus avoiding
		** duplicates.
		*/
		AND t.type = d.ss_dtype
		AND c.name like @column_name
		AND o.type != 'P'
		AND c.name like @column_name
	AND (d.ss_dtype NOT IN (111, 109, 38, 110) /* No *N types */
--	AND (d.ss_dtype NOT IN (111, 109, 38, 110, 68) /* No *N types */

			OR c.usertype >= 100) /* User defined types */
		AND m.type_name = 
		rtrim(substring(d.type_name,
		1+isnull(d.aux,
		ascii(substring("III<<<MMMI<<A<A",
		2*(d.ss_dtype%35+1)+2-8/c.length,
		1))-60), 18))

	ORDER BY TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME
end

SelectClause:
 
 /*  usertype 3 (binary) , 80 (timestamp) */
 UPDATE #results_table set COLUMN_FLAGS = 112 where usertype in (3)
 UPDATE #results_table set DATA_TYPE = 4 where DATA_TYPE = 5 and tds_type = 59
 UPDATE #results_table set DATA_TYPE = 4 where DATA_TYPE = 5 and col_len = 4
 UPDATE #results_table set COLUMN_FLAGS = 112 where COLUMN_FLAGS = 96 and usertype = 1 and DATA_TYPE = 129
 UPDATE #results_table set COLUMN_FLAGS = 112 where COLUMN_FLAGS = 96 and usertype = 34 and DATA_TYPE = 130
 UPDATE #results_table set COLUMN_FLAGS = 112 where COLUMN_FLAGS = 96 and usertype = 24 and DATA_TYPE = 129
 UPDATE #results_table set COLUMN_FLAGS = 112 where COLUMN_FLAGS = 16 and tds_type = 62 and DATA_TYPE = 5
 UPDATE #results_table set COLUMN_FLAGS = 112 where COLUMN_FLAGS = 16 and tds_type = 59 and DATA_TYPE = 4
 if(@is_ado = 1)
 begin
 select TABLE_CATALOG,
 	TABLE_SCHEMA ,
 	TABLE_NAME ,
 	COLUMN_NAME,
 	COLUMN_GUID,
 	COLUMN_PROPID,
 	ORDINAL_POSITION ,
 	COLUMN_HASDEFAULT,
 	COLUMN_DEFAULT,
 	COLUMN_FLAGS,
-- 	USERTYPE = usertype,
         IS_NULLABLE ,
 	DATA_TYPE ,
 	--TYPE_NAME,
 	TYPE_GUID ,
 	CHARACTER_MAXIMUM_LENGTH ,
        	CHARACTER_OCTET_LENGTH ,
 	NUMERIC_PRECISION ,
 --	NUMERIC_PRECISION_RADIX,
 	NUMERIC_SCALE ,
 	DATETIME_PRECISION ,
 	CHARACTER_SET_CATALOG ,
 	CHARACTER_SET_SCHEMA ,
 	CHARACTER_SET_NAME,
 	COLLATION_CATALOG,
 	COLLATION_SCHEMA,
 	COLLATION_NAME,
 	DOMAIN_CATALOG,
 	DOMAIN_SCHEMA ,
 	DOMAIN_NAME,
 	DESCRIPTION
-- 	TDSTYPE = tds_type,
-- 	COLID = id,
--	COLLEN = col_len
 	
 	FROM
 	#results_table 
 	order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION
 
 end
 else
 if(@is_ado = 2)
 begin
 select TABLE_CATALOG,
 	TABLE_SCHEMA ,
 	TABLE_NAME ,
 	COLUMN_NAME,
 	COLUMN_GUID,
 	COLUMN_PROPID,
 	ORDINAL_POSITION ,
 	COLUMN_HASDEFAULT,
 	COLUMN_DEFAULT,
 	COLUMN_FLAGS,
-- 	USERTYPE = usertype,
   	IS_NULLABLE ,
 	--DATA_TYPE ,
 	TYPE_NAME, 
 	TYPE_GUID ,
 	CHARACTER_MAXIMUM_LENGTH ,
        	CHARACTER_OCTET_LENGTH ,
 	NUMERIC_PRECISION ,
 	NUMERIC_PRECISION_RADIX,
 	NUMERIC_SCALE ,
 	DATETIME_PRECISION ,
 	CHARACTER_SET_CATALOG ,
 	CHARACTER_SET_SCHEMA ,
 	CHARACTER_SET_NAME,
 	COLLATION_CATALOG,
 	COLLATION_SCHEMA,
 	COLLATION_NAME,
 	DOMAIN_CATALOG,
 	DOMAIN_SCHEMA ,
 	DOMAIN_NAME,
 	DESCRIPTION
-- 	TDSTYPE = tds_type,
-- 	COLID = id,
--	COLLEN = col_len
 	
 	FROM
 	#results_table 
 	order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION
    end
    
    drop table #results_table
    if (@startedInTransaction = 1)
	rollback transaction oledb_keep_temptable_tx      

return(0)
go
exec sp_procxmode 'sp_oledb_columns', 'anymode'
go
grant execute on sp_oledb_columns to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go

/* ----------------ColumnPrivileges ------------------------------*/

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_getcolumnprivileges')
begin
	drop procedure sp_oledb_getcolumnprivileges
end
go


/* Sccsid = "%Z% generic/sproc/src/%M% %I% %G%" */

create procedure sp_oledb_getcolumnprivileges ( 
                        @table_name  varchar(96) = null,
                        @table_owner varchar(32) = null,
                        @table_qualifier varchar(32)= null,
                        @column_name varchar(96) = null,
                        @grantor varchar(32) = null,
                        @grantee varchar(32) = null)

as        

    declare @owner_id    		int
    declare @full_table_name    	varchar(193)
    declare @tab_id 			int	    /* object id of the table specified */
    declare @startedInTransaction bit
    if (@@trancount > 0)
  	select @startedInTransaction = 1
    else
        select @startedInTransaction = 0

    set nocount on
    /*
    ** set the transaction isolation level
    */
    if @@trancount = 0
    begin
	   set chained off
    end


    set transaction isolation level 1
    if (@startedInTransaction = 1)
	save transaction oledb_keep_temptable_tx    

    /*
    **  Check to see that the table is qualified with database name
    */
    if @table_name like "%.%.%"   /*CT: oledb can be null */
    begin
		/* 18021, "Object name can only be qualified with owner name" */
		raiserror 18021
		return (1)
    end

    /*  If this is a temporary table; object does not belong to 
    **  this database; (we should be in our temporary database)
    */
    if (@table_name like "#%" and db_name() != 'tempdb')  /*CT: tempdb*, use template*/
    begin
		/* 
		** 17676, "This may be a temporary object. Please execute 
		** procedure from your temporary database."
		*/
		raiserror 17676
		return (1)
    end

    /*
    ** The table_qualifier should be same as the database name. Do the sanity check
    ** if it is specified
    */
    if (@table_qualifier is null) or (@table_qualifier = '')
	/* set the table qualifier name */
	select @table_qualifier = db_name ()
   
   /* 
    ** if the table owner is not specified, it will be taken as the id of the
    ** user executing this procedure. Otherwise find the explicit table name prefixed
    ** by the owner id
    */
    if (@table_owner is null) or (@table_owner = '')
	        select @full_table_name = @table_name
    else
    begin
	if (@table_name like "%.%") and
	    substring (@table_name, 1, charindex(".", @table_name) -1) != @table_owner
	begin
	 	/* 18011, Object name must be qualified with the owner name * */
		raiserror 18011
		return (1)
	end
	
	if not (@table_name like "%.%")
        	select @full_table_name = @table_owner + '.' + @table_name
	else
	        select @full_table_name = @table_name

    end

   /* Create temp table to store results from sp_aux_computeprivs */
    create table #results_table
	 (table_qualifier	varchar (32),
	  table_owner		varchar (32),
	  table_name		varchar (32),
	  column_name		varchar (32) NULL,
	  grantor		varchar (32),
	  grantee 		varchar (32),
	  privilege		varchar (32),
	  is_grantable		varchar (3))
	  
    /*
    ** if the column name is not specified, set the column name to wild 
	** character such it matches all the columns in the table
    */
    if @column_name is null
        select @column_name = '%'	  
    /* 
    ** check to see if the specified table exists or not
    */
    select @tab_id = object_id(@full_table_name)
 
    if (@tab_id is not null)
	begin
	
	    /*
	    ** check to see if the @tab_id indeeed represents a table or a view
	    */

	    if exists (select * 
			  from   sysobjects
			  where (@tab_id = id) and
				((type = 'U') or
				(type = 'S') or
				(type = 'V')))
    	begin
         /*
	 ** check to see if the specified column is indeed a column belonging
	 ** to the table
	 */
         if exists (select * 
                        from syscolumns
			where (id = @tab_id) and
			      (name like @column_name))
	begin

   /*
   ** declare cursor to cycle through all possible columns
   */
   declare cursor_columns cursor
	for select name from syscolumns 
	    where (id = @tab_id) 
	      and (name like @column_name)

   /*
   ** For each column in the list, generate privileges
   */
   open cursor_columns
   fetch cursor_columns into @column_name
   while (@@sqlstatus = 0)
   begin

	/* 
	** compute the table owner id
	*/

	select @owner_id = uid
	from   sysobjects
	where  id = @tab_id


	/*
	** get table owner name
	*/

	select @table_owner = name 
	from sysusers 
	where uid = @owner_id
			     
	/*exec sp_aux_computeprivs @table_name, @table_owner, @table_qualifier, 
			     @column_name, 1, @tab_id */

	exec sp_oledb_computeprivs @table_name, @table_owner, @table_qualifier, 
			     @column_name, 1, @tab_id
			     
	set nocount off 	

	fetch cursor_columns into @column_name
   end

   close cursor_columns
   deallocate cursor cursor_columns
   end
   end
   end

   /* Print out results */ 

  if (@grantor is null) and (@grantee is null)
   select distinct GRANTOR = r.grantor,
   		   GRANTEE = r.grantee,
   		   TABLE_CATALOG = r.table_qualifier,
   		   TABLE_SCHEMA = r.table_owner,
   		   TABLE_NAME = r.table_name,
   		   COLUMN_NAME = r.column_name,
   		   COLUMN_GUID = convert(varchar(36), null),
   		   COLUMN_PROPID = convert(int,null),
   		   PRIVILEGE_TYPE = case when r.privilege = 'REFERENCE' then "REFERENCES"
      		        else r.privilege end,
		   IS_GRANTABLE = case when r.is_grantable = 'YES' then convert(bit, 1)
			else convert (bit, 0) end
   from #results_table r
   order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, PRIVILEGE_TYPE
  else
    if @grantee is null
      select distinct GRANTOR = r.grantor,
      		   GRANTEE = r.grantee,
      		   TABLE_CATALOG = r.table_qualifier,
      		   TABLE_SCHEMA = r.table_owner,
      		   TABLE_NAME = r.table_name,
      		   COLUMN_NAME = r.column_name,
      		   COLUMN_GUID = convert(varchar(36), null),
      		   COLUMN_PROPID = convert(int,null),
      		   PRIVILEGE_TYPE = case when r.privilege = 'REFERENCE' then "REFERENCES"
      		        else r.privilege end,
		   IS_GRANTABLE = case when r.is_grantable = 'YES' then convert(bit, 1)
			else convert (bit, 0) end
      from #results_table r where r.grantor = @grantor
   order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, PRIVILEGE_TYPE
   else
     if @grantor is null
           select distinct GRANTOR = r.grantor,
           		   GRANTEE = r.grantee,
           		   TABLE_CATALOG = r.table_qualifier,
           		   TABLE_SCHEMA = r.table_owner,
           		   TABLE_NAME = r.table_name,
           		   COLUMN_NAME = r.column_name,
           		   COLUMN_GUID = convert(varchar(36), null),
           		   COLUMN_PROPID = convert(int,null),
           		   PRIVILEGE_TYPE = case when r.privilege = 'REFERENCE' then "REFERENCES"
      		                else r.privilege end,
   		           IS_GRANTABLE = case when r.is_grantable = 'YES' then convert(bit, 1)
   		   		else convert (bit, 0) end
           from #results_table r where r.grantee = @grantee
   order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, PRIVILEGE_TYPE
     else
           select distinct GRANTOR = r.grantor,
           		   GRANTEE = r.grantee,
           		   TABLE_CATALOG = r.table_qualifier,
           		   TABLE_SCHEMA = r.table_owner,
           		   TABLE_NAME = r.table_name,
           		   COLUMN_NAME = r.column_name,
           		   COLUMN_GUID = convert(varchar(36), null),
           		   COLUMN_PROPID = convert(int,null),
           		   PRIVILEGE_TYPE = case when r.privilege = 'REFERENCE' then "REFERENCES"
      		                else r.privilege end,
   		           IS_GRANTABLE = case when r.is_grantable = 'YES' then convert(bit, 1)
   		   		else convert (bit, 0) end
           from #results_table r where r.grantor = @grantor and r.grantee = @grantee
   order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, PRIVILEGE_TYPE  

drop table #results_table
if (@startedInTransaction = 1)
   save transaction oledb_keep_temptable_tx    
   
return (0)
go
exec sp_procxmode 'sp_oledb_getcolumnprivileges', 'anymode'
go
grant execute on sp_oledb_getcolumnprivileges to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go


/* -------------Catalog -----------------------------*/
if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_databases')
begin
	drop procedure sp_oledb_databases
end
go


/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */

create procedure sp_oledb_databases (@database_name varchar(32) = null)
as
	if @@trancount = 0
	begin
		set chained off
	end

	set transaction isolation level 1

    if @database_name is null	
    begin
	select CATALOG_NAME = name,
		DESCRIPTION = convert(varchar(254),null)  /*no description*/
	from master.dbo.sysdatabases order by name
    end
    else
    begin
	select CATALOG_NAME = name,
		DESCRIPTION = convert(varchar(254),null)  /*no description*/
	from master.dbo.sysdatabases where name = @database_name
	order by name
    end
    
	return(0)
go
go
exec sp_procxmode 'sp_oledb_databases', 'anymode'
go
grant execute on sp_oledb_databases to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go


/*-----------------------VIEW------------------------------------------*/
if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_oledb_views')
begin
	drop procedure sp_oledb_views
end
go

create procedure sp_oledb_views
@table_catalog	 	varchar(32)  = null,
@table_schema     	varchar(32)  = null,
@table_name		varchar(96)  = null
as

declare @type1 varchar(3)
declare @tableindex int
declare @startedInTransaction bit
if (@@trancount > 0)
   select @startedInTransaction = 1
else
   select @startedInTransaction = 0

 
if @@trancount = 0
begin
	set chained off
end

set transaction isolation level 1
if (@startedInTransaction = 1)
    save transaction oledb_keep_temptable_tx    

/* temp table */
if (@table_name like "#%" and
   db_name() != 'tempdb')
begin
	/*
        ** Can return data about temp. tables only in tempdb
        */
		raiserror 17676
	return(1)
end

create table #oledb_results_table
	 (
	  TABLE_CATALOG		varchar (32) null,
	  TABLE_SCHEMA		varchar (32) null,
	  TABLE_NAME		varchar (32) null,
	  VIEW_DEFINITION       varchar (32) null,
	  CHECK_OPTION          bit default 1,
	  IS_UPDATABLE          bit default 0,
	  DESCRIPTION		varchar(32) null,
	  DATE_CREATED		datetime null,
	  DATE_MODIFIED		datetime null
	 )

/*
** Special feature #1:	enumerate databases when owner and name
** are blank but qualifier is explicitly '%'.  
*/
if @table_catalog = '%' and
	@table_schema = '' and
	@table_name = ''
begin	

	/*
	** If enumerating databases 
	*/
	insert #oledb_results_table
	select
		TABLE_CATALOG = name,
		TABLE_SCHEMA = null,
		TABLE_NAME = null,
		VIEW_DEFINITION = convert(varchar(255), null),
		CHECK_OPTION = 1,
		IS_UPDATABLE = 0,
		DESCRIPTION = convert(varchar(255), null),
		DATE_CREATED = convert(datetime, null),
	  	DATE_MODIFIED = convert(datetime, null)
		
		from master..sysdatabases

		/*
		** eliminate MODEL database 
		*/
		where name != 'model'
		order by TABLE_CATALOG
end

/*
** Special feature #2:	enumerate owners when qualifier and name
** are blank but owner is explicitly '%'.
*/
else if @table_catalog = '' and
	@table_schema = '%' and
	@table_name = ''
	begin	

		/*
		** If enumerating owners 
		*/
		insert #oledb_results_table
		select distinct			
			TABLE_CATALOG = null,
			TABLE_SCHEMA = user_name(uid),
			TABLE_NAME = null,
			VIEW_DEFINITION = convert(varchar(255), null),
			CHECK_OPTION = 1,
			IS_UPDATABLE = 0,
			DESCRIPTION = convert(varchar(255), null),
			DATE_CREATED = convert(datetime, null),
			DATE_MODIFIED = convert(datetime, null)		

		from sysobjects
		order by TABLE_SCHEMA
	end
	else
	begin 

		/*
		** end of special features -- do normal processing 
		*/
		if @table_catalog is not null
	
begin
			if db_name() != @table_catalog
			begin
				if @table_catalog = ''
				begin  	

					/*
					** If empty qualifier supplied
					** Force an empty result set 
					*/
					select @table_name = ''
					select @table_schema = ''
				end
				else
				begin

					/*
					** If qualifier doesn't match current 
					** database. 
					*/
					raiserror 18039
					return 1
				end
			end
		end	

		select @type1 = 'V'

		if @table_name is null
	
		begin	

			/*
			** If table name not supplied, match all 
			*/
			select @table_name = '%'
		end
		else
		begin
			if (@table_schema is null) and 
			   (charindex('%', @table_name) = 0)
			begin	

			/*
			** If owner not specified and table is specified 
			*/
				if exists (select * from sysobjects
					where uid = user_id()
					and id = object_id(@table_name)
					and type = 'V')
				begin	

				/*
				** Override supplied owner w/owner of table 
				*/
					select @table_schema = user_name()
				end
			end
		end

		/*
		** If no owner supplied, force wildcard 
		*/
		if @table_schema is null 
		 select @table_schema = '%'
		insert #oledb_results_table
		select
			TABLE_CATALOG = db_name(),
			TABLE_SCHEMA = user_name(o.uid),
			TABLE_NAME = o.name,
			VIEW_DEFINITION = convert(varchar(255), null),
			CHECK_OPTION = 1,
			IS_UPDATABLE = 0,
			DESCRIPTION = convert(varchar(255), null),
			DATE_CREATED = convert(datetime, null),
			DATE_MODIFIED = convert(datetime, null)		
		from sysusers u, sysobjects o
		where
			/* Special case for temp. tables.  Match ids */
			(o.name like @table_name or o.id=object_id(@table_name))
			and user_name(o.uid) like @table_schema

			/*
			** Only desired types
			*/
			and charindex(substring(o.type,1,1),@type1)! = 0 

			/*
			** constrain sysusers uid for use in subquery 
			*/
			and u.uid = user_id() 
		and (
                suser_id() = 1          /* User is the System Administrator */
                or o.uid = user_id()    /* User created the object */
                                        /* here's the magic..select the highest
                                        ** precedence of permissions in the
                                        ** order (user,group,public)
                                        */
 
                /*
                ** The value of protecttype is
                **
                **      0  for grant with grant
                **      1  for grant and,
                **      2  for revoke
                **
                ** As protecttype is of type tinyint, protecttype/2 is
                ** integer division and will yield 0 for both types of
                ** grants and will yield 1 for revoke, i.e., when
                ** the value of protecttype is 2.  The XOR (^) operation
                ** will reverse the bits and thus (protecttype/2)^1 will
                ** yield a value of 1 for grants and will yield a
                ** value of zero for revoke.
                **
	        ** For groups, uid = gid. We shall use this to our advantage.
                **
                ** If there are several entries in the sysprotects table
                ** with the same Object ID, then the following expression
                ** will prefer an individual uid entry over a group entry
                **
                ** For example, let us say there are two users u1 and u2
                ** with uids 4 and 5 respectiveley and both u1 and u2
                ** belong to a group g12 whose uid is 16390.  table t1
                ** is owned by user u0 and user u0 performs the following
                ** actions:
                **
                **      grant select on t1 to g12
                **      revoke select on t1 from u1
                **
                ** There will be two entries in sysprotects for the object t1,
                ** one for the group g12 where protecttype = grant (1) and
                ** one for u1 where protecttype = revoke (2).
                **
                ** For the group g12, the following expression will
                ** evaluate to:
                **
                **      ((abs(16390-16390)*2) + ((1/2)^1)
                **      = ((0) + (0)^1) = 0 + 1 = 1
                **
                ** For the user entry u1, it will evaluate to:
                **
                **      (((+)*abs(4-16390)*2) + ((2/2)^1))
                **      = (abs(-16386)*2 + (1)^1)
                **      = 16386*2 + 0 = 32772
                **
                ** As the expression evaluates to a bigger number for the
                ** user entry u1, select max() will chose 32772 which,
                ** ANDed with 1 gives 0, i.e., sp_oledb_tables will not display
                ** this particular table to the user.
                **
                ** When the user u2 invokes sp_oledb_tables, there is only one
                ** entry for u2, which is the entry for the group g12, and
                ** so the group entry will be selected thus allowing the
                ** table t1 to be displayed.
                **
		** ((select max((abs(uid-u.gid)*2)
	        ** 		+ ((protecttype/2)^1))
         	**
                ** Notice that multiplying by 2 makes the number an
                ** even number (meaning the last digit is 0) so what
                ** matters at the end is (protecttype/2)^1.
                **
                **/
 
                or ((select max((abs(p.uid-u2.gid)*2) + ((p.protecttype/2)^1))
                        from sysprotects p, sysusers u2
                        where p.id = o.id      /* outer join to correlate
                                                ** with all rows in sysobjects
                                                */
			and u2.uid = user_id()
			/*
			** get rows for public, current users, user's groups
			*/
		      	and (p.uid = 0 or 		/* public */
			     p.uid = user_id() or	/* current user */ 
			     p.uid = u2.gid)		/* users group */ 

			/*
			** check for SELECT, EXECUTE privilege.
			*/
		 	and (p.action in (193,224)))&1

			/*
			** more magic...normalise GRANT
			** and final magic...compare
			** Grants.
			*/
			) = 1
		/*
			** If one of any user defined roles or contained roles for the
			** user has permission, the user has the permission
			*/
			or exists(select 1
				from sysprotects p1,
					master.dbo.syssrvroles srvro,
					sysroles ro
				where p1.id = o.id
				and p1.uid = ro.lrid
				and ro.id = srvro.srid
	--	and has_role(srvro.name, 1) > 0
				and p1.action = 193))
		
		order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME
end
	select * from #oledb_results_table
	
drop table #oledb_results_table
if (@startedInTransaction = 1)
    rollback transaction oledb_keep_temptable_tx    

return (0)
go
exec sp_procxmode 'sp_oledb_views', 'anymode'
go
grant execute on sp_oledb_views to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go

print ""
go
print "Installed oledb_mda Stored Procedures ..."
go
declare @retval int
declare @version_string varchar(255) 
select @version_string = '15.0.0.325/'+ 'Thu 08-14-2008 14:58:04.98' 
exec @retval = sp_version 'OLEDB MDA Scripts', NULL, @version_string, 'end'
if (@retval != 0) select syb_quit()
go
