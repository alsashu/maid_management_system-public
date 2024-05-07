namespace MaidManagementSolutions.DataAccessLayer
{
    public class SqlStatement
    {
        #region States
        public static readonly string getStates = "SELECT Id, Name, CountryId FROM scms.states;";

        public static readonly string getStatesById = "SELECT * FROM scms.states where CountryId = @CountryId;";
        #endregion

        #region Cities
        public static readonly string getCities = "SELECT Id, Name, StateId FROM scms.cities;";
        #endregion

        #region JobTypes
        public static readonly string getJobTypes = "SELECT Id, Job FROM scms.job_type_master;";
        #endregion

        #region Langauges
        public static readonly string getLangauges = "SELECT Id, Langauge FROM scms.langauge_master;";
        #endregion

        #region Religions
        public static readonly string getReligions = "SELECT Id, Religion FROM scms.religion_master;";
        #endregion

        #region AgencyRegistartion

        #endregion

        #region Agreement

        #endregion

        #region NormalRegistartion
        public static readonly string getNormalRegistartion = "SELECT Id, FirstName, LastName, ContactNumber, Address, PhotoLink, IdCard, Password, Declaration, Gender, Role, TimePreference, Languages, WorkLocations, Reference, Comments, ProfessionallyQualifiedNurse, Email, Others, Age, Experience, Country, State, City, Religions FROM scms.normal_registartion;";

        public static readonly string getAllPhoto = "SELECT PhotoLink FROM scms.normal_registartion;";

        //public static readonly string searchFromNormalRegistartion = "SELECT * FROM scms.normal_registartion where JSON_CONTAINS(WorkLocations,'\"_QUERY_\"','$') or JSON_CONTAINS(Languages,'\"_QUERY_\"','$') or JSON_CONTAINS(Languages,'\"_QUERY_\"','$') or State = '_QUERY_' or City = '_QUERY_' or Gender = '_QUERY_' or Age  = '_QUERY_' or Experience = '_QUERY_' or Religions = '_QUERY_';";
        public static readonly string searchFromNormalRegistartion = "SELECT * FROM scms.normal_registartion where  City = '_QUERY_';";
        #endregion

        #region RegistartionByAgent

        #endregion

        #region RegistartionByAgent
        public static readonly string addContactForm = "INSERT INTO scms.contact_form (Id,Name,Email,Contact,Password,Message,Subject,FormType,CreatedOn,Others) VALUES (@Id,@Name,@Email,@Contact,@Password,@Message,@Subject,@FormType,@CreatedOn,@Others);";
        #endregion

        #region RegistartionByAgent
        public static readonly string addCustomerRegistration = "INSERT INTO scms.customer_registration (Id,Name,Email,Contact,FirstName,LastName,Password,Message,Subject,FormType,CreatedOn,Others) VALUES (@Id,@Name,@Email,@Contact,@FirstName,@LastName,@Password,@Message,@Subject,@FormType,@CreatedOn,@Others);";
        #endregion

        #region Maid
        ///Insert
        public static readonly string addMaid = "INSERT INTO public.maid(id, firstname, lastname, contactnumber, email, address, countryid, stateid, state, city, community, location, about, pincode, isverified, isblocklisted, createdby, createddate, modifiedby, modifieddate, eduction, age, availablefrom, experienceinyears, experienceinmonths, previousemployername, previousemployercontactnumber,gender,workinghours, maritalstatus, languages, services) VALUES (@id, @firstname, @lastname, @contactnumber, @email, @address, @countryid, @stateid, @state, @city, @community, @location, @about, @pincode, @isverified, @isblocklisted, @createdby, @createddate, @modifiedby, @modifieddate, @eduction, @age, @availablefrom, @experienceinyears, @experienceinmonths, @previousemployername, @previousemployercontactnumber,@gender,CAST(@workinghours AS json), @maritalstatus,CAST(@languages AS json), CAST(@services AS json));";
        //Search
        public static readonly string findMaid = "SELECT * FROM public.maid WHERE city ILIKE 'value1' OR address ILIKE '%value2%' OR gender ILIKE '%value2%';";
        
        public static readonly string getMaid = "SELECT * FROM public.maid;";

        public static readonly string getAllMaid = "SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) where filetype = 'photo';";
        
        public static readonly string getMaidById = "SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) where filetype = 'photo' and md.id=CAST(@id AS UUID);";

        //public static readonly string addInitial = "INSERT INTO public.maid(id, firstname, lastname, contactnumber, email, address, countryid, stateid, state, city, community, location, about, pincode, isverified, isblocklisted, createdby, createddate, modifiedby, modifieddate, eduction, age, availablefrom, experienceinyears, experienceinmonths, previousemployername, previousemployercontactnumber,gender,workinghours, maritalstatus, languages, services) VALUES (@id,@firstname, @lastname, @contactnumber, @email, @address, @countryid, @stateid, @state, @city, @community, @location, @about, @pincode, @isverified, @isblocklisted, @createdby, @createddate, @modifiedby, @modifieddate, @eduction, @age, @availablefrom, @experienceinyears, @experienceinmonths, @previousemployername, @previousemployercontactnumber,@gender,@workinghours, @maritalstatus,CAST(@languages AS json), CAST(@services AS json));";

        public static readonly string isContactAvailable = "SELECT * FROM public.maid where contactnumber = @contactnumber;";
        
        public static readonly string updateMaidOne = "UPDATE public.maid SET modifiedby=@modifiedby, modifieddate=@modifieddate, age=@age, experienceinyears=@experienceinyears, experienceinmonths=@experienceinmonths,  gender=@gender, workinghours= CAST(@workinghours AS json), languages=CAST(@languages AS json), services=CAST(@services AS json) WHERE id = @id;";
        
        public static readonly string updateMaidTwo = "UPDATE public.maid SET about=@about,address=@address,state=@state, city=@city, community=@community, location=CAST(@location AS json), pincode=@pincode, modifiedby=@modifiedby, modifieddate=@modifieddate,availablefrom=@availablefrom WHERE id=@id;";
        
        public static readonly string addEmployer = "UPDATE public.maid SET previousemployer=CAST(@previousemployer AS json) WHERE id = @id;";

        public static readonly string getOnlyMaidById = "SELECT * FROM public.maid WHERE id=@id;";

        public static readonly string filterMaid = @"SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) WHERE doc.filetype='photo' and community ILIKE '%$COMMUNITY$%' OR location ILIKE '%$LOCATION$%';";

        public static readonly string filterMaidLocation = @"SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) WHERE doc.filetype='photo' and location ILIKE '%$LOCATION$%';";
        
        public static readonly string filterMaidCommunity = @"SELECT * FROM public.maid as md inner join public.documents as doc on md.id=CAST(doc.filename AS UUID) WHERE doc.filetype='photo' and community ILIKE '%$COMMUNITY$%';";

        #endregion

        #region document
        //InsertDoc
        public static readonly string addDocument = "INSERT INTO public.documents(id, filename, filedata, filetype) VALUES (@id, @filename, @filedata, @filetype);";
        //GetDocumentByFileName
        public static readonly string getDocumentByFileName = "SELECT id, filename, filedata, filetype FROM public.documents where filename=@filename;";
        
        //public static readonly string getDefaultFile = "SELECT id, filename, filedata, filetype FROM public.documents where id='e0b97752-c41f-45cb-9f2c-3c8eefa85ce2';";
        public static readonly string getDefaultFile = "SELECT id, filename, filedata, filetype FROM public.documents where id='08249abb-f9b8-49b0-a65f-0192c6adf26e';";
       
        #endregion


        #region user
        //InsertDoc
        public static readonly string addUser = "INSERT INTO public.users(id,username,password,phonenumber,istermsaccepted, registrationdate, isativeuser, oldpassword, updatedon) VALUES (@id,@username,@password,@phonenumber,@istermsaccepted, @registrationdate, @isativeuser,CAST(@oldpassword AS json), @updatedon);";
        //GetDocumentByFileName
        public static readonly string getActiveUser = "SELECT id, username, phonenumber FROM public.users where isativeuser=true and phonenumber=@phonenumber and password = @password;";
        
        public static readonly string isUserAvailable = "SELECT id, username, phonenumber FROM public.users where phonenumber=@phonenumber;";
        #endregion

        #region review
        public static readonly string addReview = "INSERT INTO public.review(id, maidid, clientid, rating, review, reviewdate, reviewedby, modifieddate, modifiedby, title) VALUES(@id, @maidid, @clientid, @rating, @review, @reviewdate, @reviewedby, @modifieddate, @modifiedby, @title);";
        public static readonly string getAllReview = "SELECt * FROM  public.review WHERE maidid = @maidid;";

        public static readonly string getAverageReview = "SELECT count(1) AS total, ROUND(AVG(rating), 1) AS average FROM public.review where maidid = @maidid;";
        
        public static readonly string getGroupCount = "SELECT rating, count(1) AS total FROM public.review  where maidid = @maidid group by rating  order by rating desc;";

        public static readonly string getGroupPercentage = "SELECT rating, COUNT(*) AS total, ROUND((COUNT(*) * 100.0) / SUM(COUNT(*)) OVER (), 2) AS percentage FROM public.review WHERE maidid = @maidid GROUP BY rating ORDER BY rating DESC;";
        #endregion

        #region custsugg
        public static readonly string addcustsuggestion = "INSERT INTO public.custsugg(id, custsuggestion) VALUES (@id, @custsuggestion);";
        #endregion

        public static readonly string addReportProfile = "INSERT INTO public.reportprofile(id, clientid, maidid, reportingcomment, reportedon) VALUES (@id, @clientid, @maidid, @reportingcomment, @reportedon);";
    }
}
