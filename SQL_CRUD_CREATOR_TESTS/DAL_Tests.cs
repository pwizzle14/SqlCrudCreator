using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoldenvaleDAL.DataLayerWorker;
using GoldenvaleDAL.ClassObjects;
using System.Collections.Generic;

namespace SQL_CRUD_CREATOR_TESTS
{
   [TestClass]
    public class DAL_Tests
    {
        private IDataLayerWorker _databaseService = new DataLayerWorker();

        private Persons _fakePerson
        {
            get
            {
                return new Persons()
                {
                    PersonID = 0,
                    FirstName = "Test FirstName2",
                    LastName = "Test Lastname",
                    Address = "123 Fake St",
                    City = "FakeVille"

                };
            }
        }

        [TestMethod]
        public void PersonsAddTest()
        {
            var per = _fakePerson;

            var results = _databaseService.Create(per);

            var personId = results.Result.PersonID;

            Assert.IsNotNull(personId);
            Assert.AreNotEqual(0, personId);

            _databaseService.DeleteById(personId, new Persons());

        }

        [TestMethod]
        public void PersonsUpdateTest()
        {
            var personToCreate = _fakePerson;

            personToCreate.FirstName = "PersonsUpdateTest";

            var createResults = _databaseService.Create(personToCreate);

            Assert.IsNotNull(createResults.Result);

            var selectPersonResults = _databaseService.SelectById<Persons>(createResults.Result.PersonID, new Persons());

            Assert.IsNotNull(selectPersonResults.Result);

            Persons selectPerson =selectPersonResults.Result;

            selectPerson.FirstName = "Updated";

            _databaseService.Update(selectPerson);

            var finalPersonResults = _databaseService.SelectById<Persons>(createResults.Result.PersonID, new Persons());
            var finalPerson = finalPersonResults.Result;

            Assert.AreNotEqual(finalPerson.FirstName, personToCreate.FirstName);

            _databaseService.DeleteById<Persons>(createResults.Result.PersonID, new Persons());

        }

        [TestMethod]
        public void PersonDeleteTest()
        {
            var createdPersonRes = _databaseService.Create(_fakePerson);

            var personId = createdPersonRes.Result.PersonID;

            _databaseService.DeleteById<Persons>(personId, new Persons());

            var deletedRow = _databaseService.SelectById<Persons>(personId, new Persons()).Result;

            Assert.IsNull(deletedRow);
        }

        [TestMethod]
        public void PersonSelectByIdTest()
        {
            var id = _databaseService.Create(_fakePerson).Result.PersonID;
            
            var results = _databaseService.SelectById<Persons>(id, new Persons());

            Assert.IsNotNull(results);

            _databaseService.DeleteById(id, new Persons());
        }

        [TestMethod]
       
        public void PersonsSelectByLastNameTest()
        {
            var parms = new Dictionary<string, object>
            {
                { "@LastName", "N" }
            };


            var results = _databaseService.ExecuteSproc<Persons>(Persons.FetchByNameSproc(), parms);

            var searchResults = results.Result;

            Assert.AreNotEqual(0,searchResults.Count);

        }
    }
}
