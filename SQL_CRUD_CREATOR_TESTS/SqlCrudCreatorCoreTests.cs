using SqlCrudCreatorCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlCrudCreatorCore.BL;
using SqlCrudCreatorCore.Services;
using SqlCrudCreatorCore.CRUD_Templates.SQL;
using NSubstitute;
using SQL_CRUD_CREATOR_TESTS.Services;
using SqlCrudCreatorCore.DAL;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SQL_CRUD_CREATOR_TESTS
{
    [TestClass]
    public class SqlCrudCreatorCoreTests
    {
        /// <summary>
        /// These tests cover creating the text that will be written to the files. 
        /// </summary>


        [TestMethod]
        public void CreateAllClassAndScriptsTest()
        {
            //arrange
            IDatabaseService service = new FakeDataService();


            var crud = new SqlCrudCreator();

            //act
            var res = crud.CreateAllClassObjAndSQL(service, "TitanProcesserQueue", "TitanProcesserQueue", "TitanProcesserQueue");
           
            //Assert
            Assert.IsNotNull(res.SqlQuries);
            Assert.IsNotNull(res.ClassObjects);


        }
    }
}
