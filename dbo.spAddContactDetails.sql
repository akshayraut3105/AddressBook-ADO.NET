Create procedure dbo.spAddContactDetails
	@FirstName	varchar(150),		
	@LastName	varchar(150),	
	@AddressDetails	varchar(150),		
	@City	varchar(150),	
	@StateName	varchar(150),	
	@Zip	int,
    @PhoneNo bigint,
	@Email	varchar(150),	
	@addressBookType	varchar(150),
	@addressBookName	varchar(150)
	as begin
	Insert into Address_Book values(@FirstName,@LastName,@AddressDetails,@City,@StateName,@Zip,@PhoneNo,@Email,@addressBookType,@addressBookName)
	End