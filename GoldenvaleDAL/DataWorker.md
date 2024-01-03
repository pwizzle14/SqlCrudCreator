/*
 * DataLayerWorker.cs
 * 
 * Description:
 * This class provides a data layer implementation for CRUD (Create, Read, Update, Delete) operations
 * and custom stored procedure executions using Dapper and SQL Server.
 *
 * Usage:
 * 1. Create an instance of DataLayerWorker.
 * 2. Use the provided methods for CRUD operations and custom stored procedure executions.
 *
 * Connection String:
 * Modify the _connectionString variable to set the appropriate SQL Server connection details.
 *
 * Example:
 * var dataLayerWorker = new DataLayerWorker();
 * var objectToCreate = new YourObject(); // Replace YourObject with the actual object type
 * var createdObject = await dataLayerWorker.Create(objectToCreate);
 *
 * Dependencies:
 * - Dapper
 * - System.Data
 * - System.Data.SqlClient
 * - GoldenvaleDAL.Utilities
 *
 * Note: Ensure that the required dependencies are installed in your project.
 */
