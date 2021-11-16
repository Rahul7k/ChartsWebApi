# Contributing to APIs

:+1::tada: First off, thanks for taking the time to contribute! :tada::+1:

The following is a set of guidelines for contributing to API development. These are mostly guidelines, not rules. Use your best judgment, and feel free to propose changes to this document in a pull request.

#### Table Of Contents

[Styleguides](#styleguides)

- [Git Commit Messages](#git-commit-messages)
- [REST Naming Guidelines](#rest-naming-guidelines)
- [C# Styleguide](#csharp-styleguide)
- [Database Styleguide](#database-styleguide)



## Styleguides

### Git Commit Messages

Very important to add proper messages on each commit, so the team members and yourself can make out what is being done.

Please follow these:

- Always start message with the Story Id, Bug Id from the JIRA   

    > For e.g. "[TDP-15] [TDP-17] Added a new endpoint to check email uniqueness during a resource creation"

- The message should be complete, and should use following format -

    > [StoryID] [SubTaskID] [Fixed/Added/Changed/Removed] [Method/Class/Enum/Service/Mapper] for [Purpose]

    > For e.g. [TDP-15] [TDP-17] Fixed a logical error in CalculateFare inside BookingService.cs, which was causing Fare being calculated doubled.

- Avoid tagging multiple JIRA IDs in one commit. Ideally on Ticket ID for each commit. 


### REST Naming Guidelines

Please follow the instructions as provided in [https://restfulapi.net/resource-naming](https://restfulapi.net/resource-naming/)

Please use following HTTP Methods appropriatly -

- GET (for getting one record or mutliple)
- POST (for creating new record)
- PUT (for updating the record)
- PATCH (for partial updates, like status updates)
- DELETE (for deleting a record, do not use for marking a record Inactive )

#### Guidelines
1. List out the name of entites which are going to be consistent across endpoints.

    >> For e.g. name is same for entity **Booking** in either endpoint '/bookings/:id' or '/passengers/:id/bookings'

2. Pluralize the name when you use it
3. Avoid using verbs in API Enpoints. Consistently should use - 

    >> '/&lt;entity-name&gt;' and '/&lt;entity-name&gt;/:id'

4. There can be exception to above rule. Specify clear actions there - 

    >> For e.g. '/users/check-email-uniqueness'

    Avoid general actions - 

    >> For e.g. '/users/list' is not acceptable, should be '/users' only on a GET or POST. 
        '/users/update' is not acceptable, should be '/users/:id' with a PUT.



### C# Styleguide

- Naming Conventions -
    - Each Class Name, Property Name, Function Name should be Pascal Case
    - Each private Field in a Class should be Camel Case and should have a "_" as prefix
    - Each Function variable and argument should be a Camel Case
    - Names of interfaces start with I, e.g. IInterface
    - Filenames and directory names are PascalCase
    - The file name should be the same as the name of the class/interface/enum/struct it contains

- Worker Classes (defining such type, use following as suffix while naming classes) - 
    - Controller: An API Controller serving an Endpoint, should always be named with Suffix "Controller"
    - Service: An orchestrator executing business logic, by fetching data from Repositories and applying transformation to create different types, using Factories
    - Factory: An implementation, which transforms data from one shape to another
    - Repository: An implementation, which makes call to get data from Database Tables using an ORM object or any other method.
    - Validator: An implementation, which generally is a one-to-one with a DTO, to perform validations on Posted Data

- ORM Entities/Domain -  
    - To be under namespace "Domain"  
    - Each Domain by convention should be named matching Table Name  
    - Each Property Name and Class Name to follow PascalCase  
    - Avoid using Data Annotations to setup ORM configurations, instead use Fluent Mappings  

- DTOs used to exchange data between client/server are called ViewModels -  
    - Under ViewModels, you can have a 
        - ViewModel (for display),  
        - EditModel (for POST/PUT),  
        - ListModel (for a collection model)  
    - Do not use Domain as ViewModel   
    - Do not extend ViewModel from a Domain

- Whitespace Rules - 
    - A maximum of one statement per line.
    - A maximum of one assignment per statement.
    - Indentation of 4 spaces, one tab.
    - Column limit: 100.
    - No line break before opening brace.
    - No line break between closing brace and else.
    - Braces used even when optional.
    - Space after if/for/while etc., and after commas.
    - No space after an opening parenthesis or before a closing parenthesis.
    - No space between a unary operator and its operand. One space between the operator and each operand of all other operators.

- Towards Better Code Writing
    - Always give descriptive names to the variables, functions, classes etc. Avoid using abbreviations
    - Always give descriptive names when using variables in iterations or LINQ queries as well. Avoid using one letter substitute
    - Always use implicit typing when RHS is a obvious assignment, otherwise define types explicitly
    - Better Expressions: Avoid using long (or nested) ternary expressions
    - Better Expressions: Avoid writing long object paths, substitute with a variable
    - Better Functions: Functions should not have a long list of arguments.
    - Better Functions: If a function name contains cojunctions like "and", "or", it can be a hint of violating Single Responsibility
    - Better Functions: Function Names should be precise and use standard/consistent verbs. 
    
        > For e.g. decide whether to use "get" or "fetch", "update" or "modify"
        
- Clean Code Rules  
	- Appropriate names
	- Descriptive names
	- Functions - 
		1. Small
		2. Precise to the purpose
		3. Single responsibility
		4. One Level of abstraction
		5. Separate Commands from Queries
	- Always declare constants for literals




### Database Styleguide
- Setup a separate user to be used for the database connection, instead of using root user

- Table Names should be singular and not plural. The name represents one record of the table.

    > For e.g. Person and not people, Account not Accounts, Invoice not Invoices etc.

- For Association table, avoid using any suffix or prefix in the table name.

    > For e.g. For payments against Invoices you can have InvoicePayment, address of a customer you can have CustomerAddress. Avoid using InvoicePaymentMap or InvoicePaymentAssociation etc.

- Both Table Name and Column Names should follow PascalCase, and no underscore or no hyphen(dash)

- Foreign Key Column Names should follow proper convention, i.e. Id in suffix to TableName.

    > For e.g. Column for Invoice reference in Payment table to be named InvoiceId, Address in Customer should be AddressId etc.

    There can be exceptions to this, if two columns in the table reference the same second table, then you can tie the purpose For e.g. CreatedById, UpdatedById etc.
