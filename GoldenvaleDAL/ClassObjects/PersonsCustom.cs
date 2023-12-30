/*
Contains custom sproc names that return a record from the table Tables.
The Persons.cs class can be re generated using SqlCrudCreator. 
 */

namespace GoldenvaleDAL.ClassObjects
{
    public partial class Persons
    {
        public static string FetchByNameSproc()
        {
            return "Persons_FetchByLastName";
        }
    }
}
