/* Master only, Execute */
/*
** Generated by spgenmsgs.pl on Thu Sep 16 22:53:10 2004 
*/
/*
** raiserror Messages for version [Total 6]
**
** 18524, "%1!: Permission denied. This operation requires System Administrator (sa_role) role."
** 19194, "Argument '%1!' is either invalid or non-unique. Valid arguments are: %2!"
** 19378, "Delete from the table %1! affected %2! rows but expected %3! rows to be deleted. Command aborted."
** 19379, "Update of the table %1! affected %2! rows but expected %3! rows to be updated. Command aborted."
** 19380, "Error in accessing the table %1!."
** 19381, "Invalid argument to %1!. It requires a non-null value."
*/
/*
** sp_getmessage Messages for version [Total 0]
*/
/*
** End spgenmsgs.pl output.
*/
/* Sccsid = "%Z% generic/sproc/%M% %I% %G%" */
/*      5.0     1.0     09/10/04      sproc/src/version */

/*
** This procedure SP_VERSION is used to find the
** version of the install scripts. In addition to 
** this it also gives the details of the successfull
** installation such as begining and ending Date with
** Time of the installation.
**
** This procedure uses the master.dbo.sysattributes
** to store all the required information. The
** columns used for storing the information is
** as follows
**
**	Column Name	Information stored
**	------------	------------------
**	object_cinfo 	Script file name
**	char_value	Version String
**	comments	Date and Time info.
**	int_value	Code value
**			(used internally)
**	
** In sysattributes this procedure will access the rows
** defined under the class 23.
**	
** Usage:
** ------
** sp_version [@Scriptfile [, @verbose [, @version [, @code ] ] ] ]
**
** Returns:
**	 0	 - Successful execution.
**	 1	 - Invalid options.
**	 2	 - Insert/Update failures.
**
*/
use sybsystemprocs
go
sp_configure 'allow updates',1
go
if (EXISTS (select name from sysobjects where id=object_id('sp_version')))
begin	
drop procedure sp_version
end
go
print 'Installing sp_version...'
go
create procedure sp_version
	@script_file	varchar(255)	= NULL,
	@verbose	varchar(3)	= NULL,
	@version	varchar(255)	= NULL,
	@code		varchar(5)	= NULL	
as

declare @class 		int
	, @check_exist	int
	, @date		varchar(150)
	, @start_date	varchar(150)
	, @end_date	varchar(150)
	, @featurecode	varchar(2)
	, @attr_code	int
	, @loc_error	int
	, @loc_rowcount	int

select @class = 23
	, @check_exist = 0
	, @start_date = NULL
	, @end_date =  NULL
	, @featurecode = 'sv'
	, @attr_code = 2	-- randomly choosen for this feature

/*
** If code = NULL means it is in reportin mode.
*/

if (@code IS NULL)	
begin	--{
	/*
	** Check whether the information is for
	** a specific install script. If so return
	** the required information. Otherwise return
	** all the rows with class = 23.
	*/
	if(@version IS NULL)
	begin --{
		select object_cinfo as Script, char_value as Version,
			comments as 'Start_End_Date', 
			case int_value
			when 0 then 'Incomplete'
			else 'Complete'
			end as 'Status'
		into #sysattributes
		from master.dbo.sysattributes
		where class = @class and object_type = @featurecode
		  and attribute = @attr_code
		  and (@script_file IS NULL
			or object_cinfo like @script_file)
	
		/*
		** If verbose option is set then print all the information 
		** including the date and time information.
		*/

		if (@verbose IS NULL)
		begin
			select Script, Version, Status  from #sysattributes order by 1
		end
		else if (@verbose = 'all')	
		begin
			select Script, Version, Status,'Start/End Date'= Start_End_Date from #sysattributes order by 1
		end
		else
		begin
			raiserror 19194, 'verbose','all'
			return(1)
		end 
	end --}
	else
	begin --{
		/* 
		** version is non-null means it is updating / insert mode
		** raise appropriate error messages for script file and
		** code value.
		*/
		if (@script_file IS NULL)
			raiserror 19381, 'script_file'
		raiserror 19194, 'code', "'start', 'end'."
		return (1)
	end --}
return (0)
end	--}
else if (@version IS NULL or @script_file IS NULL)
begin
	/*
	** If code is not NULL then check if script_file or version is NULL.
	** If any of them are NULL then give an appropriate error message.
	*/
	if (@version IS NULL) 
		raiserror 19381, 'version'
	if (@script_file IS NULL)
		raiserror 19381, 'script_file'
	if (@code != 'start' and @code != 'end')
		raiserror 19194, 'code', "'start', 'end'"
	return (1)
end
else
begin
	/*
	** If all the above conditions are satisfied and code is not NULL
	** then we need to insert / update the version string. To do this
	** check whether the use has 'sa_role'.
	*/
	if (proc_role('sa_role') = 0)
	begin
		raiserror 18524, 'sp_version'
		return (1)
	end
end

if (@code = 'start')	
begin	--{
	/*
	** code = 'start' means the begining of the installation (install
	** script). Before adding a new row to sysattributes check
	** for the previous rows and take appropriate action.
	*/
	
	begin tran update_version_string
	
	delete from master.dbo.sysattributes
	where class = @class and object_type = @featurecode
	  and attribute = @attr_code
	  and object_cinfo = @script_file

	select @loc_error = @@error, @loc_rowcount = @@rowcount	
	
	if (@loc_error != 0)	--{
	begin
		raiserror 19380, 'sysattributes'
		rollback update_version_string
		return (2)
	end	--}

	if (@loc_rowcount > 1)	--{
	begin
		raiserror 19378, 'sysattributes', @@rowcount, '1'
		rollback update_version_string
		return (2)
	end	--}

	insert into master.dbo.sysattributes(class, object_type, 
	attribute, object_cinfo, char_value, int_value, comments)
	  values(@class, @featurecode, @attr_code, @script_file,
	     @version, 0, '[Started=' + convert(varchar, getdate())+']')

	if (@@error != 0)
	begin
		raiserror 19380, 'sysattributes'
		rollback update_version_string
		return (2)
	end

	commit tran update_version_string
	return (0)
end	--}
else if (@code = 'end')	
begin	--{
	/*
	** @code = 'end' represents the successfull installation
	** of the script. Change int_value to reflect this
	** Before updating check whether the script is 
	** is registered for the installation. If so update
	** row for the script file with int_value = 0.
	** Otherwise raise an error.
	*/

	begin tran complete_version_string

	/*
	** The script is registered at the begining of the
	** Installtaion.
	*/

	select @date = convert(varchar, getdate())
	
	select @start_date=comments
	from master.dbo.sysattributes
	where class=@class and attribute = @attr_code
	  and object_type = @featurecode
	  and object_cinfo=@script_file
	

	if (@start_date IS NOT NULL)	--{
	begin
		select @end_date = @start_date
			+ '-[Completed=' + @date+']'

		update master.dbo.sysattributes
		set int_value = 1, comments=@end_date
		where class=@class and attribute = @attr_code
		  and object_type = @featurecode
		  and object_cinfo=@script_file

		select @loc_error = @@error, @loc_rowcount = @@rowcount	

		if (@loc_error != 0 ) --{	
		begin
			raiserror 19380, 'sysattributes'
			rollback complete_version_string
			return (2)
		end	--}

		if (@loc_rowcount > 1) --{	
		begin
			raiserror 19379, 'sysattributes', @@rowcount, '1'
			rollback complete_version_string
			return (2)
		end	--}
	end	--}
	else
	begin	--{	

		select @end_date = '[Completed=' + @date+']'

		insert into master.dbo.sysattributes
		(class, object_type, attribute, object_cinfo, char_value, 
							int_value, comments)
		values(@class, @featurecode, @attr_code, @script_file, 
							@version, 1, @end_date)
		
		if (@@error != 0)	--{
		begin
			raiserror 19380, 'sysattributes'
			rollback complete_version_string
			return (2)
		end	--}

	end	--}
	
	commit tran complete_version_string
	return (0)
end	--}
else	
begin
	raiserror 19194, 'code', "'start', 'end'"
	return (1)
end
go
exec sp_procxmode 'sp_version','anymode'
go
grant execute on sp_version to public
go
dump tran master with truncate_only
go
dump transaction sybsystemprocs with truncate_only
go
sp_configure 'allow updates',0
go
print 'Installed sp_version...'
go