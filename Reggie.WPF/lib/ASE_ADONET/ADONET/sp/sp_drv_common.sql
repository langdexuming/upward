use sybsystemprocs 
go
 
if exists (select * from sysobjects where name = 'sp_drv_column_default')
begin
        drop procedure sp_drv_column_default
end
go
/** SECTION END: CLEANUP **/

create procedure sp_drv_column_default
/* Don't delete the following line. It is the checkpoint for sed */
/* Server dependent stored procedure add here ADDPOINT_COL_DEFAULT */
     (@obj_id int, @default_value varchar (1024) output)
as
    declare @text_count                      int
    declare @default_holder                  varchar (255)
    declare @rownum                          int
    declare @default_starts                  int
    declare @create_default_starts           int
    declare @actual_default_starts           int
    declare @as_starts                       int
    declare @length                          int
    declare @check_case_one                  int
    declare @lf_plus_null_chars              char (2)
    declare @last_two_chars                  char (2)
    declare @check_last_two_chars            int

    /* make sure @default_value starts out as empty */
    select @default_value = null

    /* initialize @check_case_one to false (0) */
    select @check_case_one = 0

    /* initialize @check_last_two_chars to false (0) */
    select @check_last_two_chars = 0

    /* initialize the @lf_plus_null_chars variable to linefeed + null */
    select @lf_plus_null_chars = char (10) + char (0)

    /* Find out how many rows there are in syscomments defining the 
       default. If there are none, then we return a null */
    select @text_count = count (*) from syscomments
        where id = @obj_id

    if @text_count = 0
    begin
        return 0
    end

    /* See if the object is hidden (SYSCOM_TEXT_HIDDEN will be set).
       If it is, best we can do is return null */
    if exists (select 1 from syscomments where (status & 1 = 1)
        and id = @obj_id)
    begin
        return 0
    end

    select @rownum = 1
    declare default_value_cursor cursor for
        select text from syscomments where id = @obj_id
        order by number, colid2, colid

    open default_value_cursor

    fetch default_value_cursor into @default_holder
 
    while (@@sqlstatus = 0)
    begin

        if @rownum = 1
        begin
            /* find the default value
            **  Note that ASE stores default values in more than one way:
            **    1. If a client declares the column default value in the
            **       table definition, ASE will store the word DEFAULT (in
            **       all caps) followed by the default value, exactly as the
            **       user entered it (meaning it will include quotes, if the
            **       value was a string constant). This DEFAULT word will
            **       be in all caps even if the user did something like this:
            **           create table foo (col1 varchar (10) DeFaULT 'bar')
            **    2. If a client does sp_bindefault to bind a default to
            **       a column, ASE will include the text of the create default
            **       command, as entered. So, if the client did the following:
            **           create DeFAULt foo aS 'bar'
            **       that is exactly what ASE will place in the text column
            **       of syscomments.
            **       In this case, too, we have to be careful because ASE
            **       will sometimes include a newline character followed by
            **       a null at the end of the create default statement. This
            **       can happen if a client uses C isql to type in the
            **       create default command (if it comes in through java, then
            **       the newline and null are not present).
            **  Because of this, we have to be careful when trying to parse out
            **  the default value. */

            select @length = char_length (@default_holder)
            select @create_default_starts =
                charindex ('create default', lower(@default_holder))
            select @as_starts = charindex(' as ', lower(@default_holder))
            select @default_starts = charindex ('DEFAULT', @default_holder)

            if (@create_default_starts != 0 and @as_starts != 0)
            begin

                /* If we get here, then we likely have case (2) above.
                ** However, it's still possible that the client did something
                ** like this:
                **     create table foo (col1 varchar (20) default
                **         'create default foo as bar')
                ** The following if block accounts for that possibility  */

                if (@default_starts != 0 and
                  @default_starts < @create_default_starts)
                begin
                    select @check_case_one = 1
                end
                else
                begin
                    select @actual_default_starts = @as_starts + 4
                    select @check_last_two_chars = 1

                    /* set @default_starts to 0 so we don't fall into the
                    ** next if block. This is important because we would
                    ** fall into the next if block if a client had used the
                    ** following sql:
                    **     CREATE DEFAULT foo as 'bar' */
                    select @default_starts = 0
                end
            end

            if (@default_starts != 0 or @check_case_one != 0)
                /* If we get here, then we have case (1) above */

                select @actual_default_starts = @default_starts + 7

            /* The ltrim removes any left-side blanks, because ASE appears
            ** to insert several blanks between the word DEFAULT and the
            ** start of the default vale */

            select @default_holder =
                ltrim(substring
                    (@default_holder, @actual_default_starts, @length))

        end

        select @default_value = @default_value + @default_holder
        select @rownum = @rownum + 1

        fetch default_value_cursor into @default_holder

    end /* while loop */

    close default_value_cursor

    /* trim off any right-side blanks */
    select @default_value = rtrim (@default_value)

    /* trim off the newline and null characters, if they're the last
    ** two characters in what remains */
    if (@check_last_two_chars = 1)
    begin

        select @length = char_length (@default_value)
        select @last_two_chars = substring (@default_value, (@length - 1), 2)
        if (@last_two_chars = @lf_plus_null_chars)
            select @default_value = substring (@default_value, 1, (@length - 2))
    end

    return 0


go
exec sp_procxmode 'sp_drv_column_default', 'anymode'
go
grant execute on sp_drv_column_default to public
go
dump transaction sybsystemprocs with truncate_only 
go

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_drv_typeinfo')
begin
	drop procedure sp_drv_typeinfo
end
go


create procedure sp_drv_typeinfo(@sstype int=0)
as
        declare @curiso int
        select @curiso=@@isolation
        if @@isolation = 0
        begin
               
                set transaction isolation level 1
        end

		if @sstype = 0
        	select literal_prefix, literal_suffix, case_sensitive, searchable, unsigned_attribute, num_prec_radix, ss_dtype from sybsystemprocs.dbo.spt_datatype_info
        else
        	select literal_prefix, literal_suffix, case_sensitive, searchable, unsigned_attribute, num_prec_radix from sybsystemprocs.dbo.spt_datatype_info where ss_dtype = @sstype
        /*Not necessary, just for more clear logic */
        if @curiso = 0
        begin
                set transaction isolation level 0
        end


        return(0)
go

exec sp_procxmode 'sp_drv_typeinfo', 'anymode'
go
grant execute on sp_drv_typeinfo to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go


if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_localtypename')
begin
	drop procedure sp_localtypename
end
go

create procedure sp_localtypename(@sstype int)
as

	declare @curiso int
	select @curiso=@@isolation
	if @@isolation = 0
	begin
		
		set transaction isolation level 1
	end

	select local_type_name from sybsystemprocs.dbo.spt_datatype_info where ss_dtype = @sstype	

	if @curiso = 0
	begin
		set transaction isolation level 0
	end
	return(0)
go

exec sp_procxmode 'sp_localtypename', 'anymode'
go
grant execute on sp_localtypename to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go


/*
 * sp_drv_getsortdorder - to check if the default sortorder is case-insensitive.
 * if the stored procedure returns '0' - means the default sortorder if case-sensitive.
 * if the stored procedure returns >0 value - mean the default sortorder is case-insensitive.
*/

if exists (select *
	from sysobjects
		where sysstat & 7 = 4
			and name = 'sp_drv_getsortorder')
begin
	drop procedure sp_drv_getsortorder
end
go

create procedure sp_drv_getsortorder
as
declare @id int
select @id=id from master.dbo.syscharsets 
	where id=(select value from master.dbo.sysconfigures where name like 'default sortorder id') 
	and description like '%insensitive%'
if isnull(@id,0)=0
begin
 select @id=0
end
select @id
go

exec sp_procxmode 'sp_drv_getsortorder', 'anymode'
go
grant execute on sp_drv_getsortorder to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go
