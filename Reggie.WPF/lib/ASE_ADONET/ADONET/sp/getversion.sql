set flushmessage off
declare @serverversion varchar(15)
select @serverversion = substring(substring(@@version,charindex("/",@@version)+1,datalength(@@version)),1,charindex("/",substring(@@version,charindex("/",@@version)+1,datalength(@@version))) - 1)

 if  ( @serverversion < "12.5")
 	begin
 		select @serverversion = "120"
		
 	end
 else
 	if  ( @serverversion >= "12.5.1" and @serverversion < "15.0")
 		begin
 			select @serverversion = "1251"
 		end
 	else
 		if  ( @serverversion >= "12.5" and @serverversion < "12.5.0.3")
 			begin
				select @serverversion = "125"
			end
		else 
			if  ( @serverversion >= "12.5.0.3" and @serverversion < "12.5.1")
			begin
				select @serverversion = "12503"
			end
			else
			if  ( @serverversion >= "15.0")
			begin
				select @serverversion = "150"
			end



print "set ASEVERSION=%1!",@serverversion 

go