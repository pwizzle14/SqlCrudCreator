﻿namespace GoldenvaleDAL.ClassObjects
{
	public class Persons : iDataLayerObj
	{

		public int PersonID { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }


		public string SprocNameCreate()
		{
			return "Persons_Create";
		}
		public string SprocNameUpdate()
		{
			return "Persons_Update";
		}
		public string SprocNameDelete()
		{
			return "Persons_Delete";
		}
		public string SprocNameFetch()
		{
			return "Persons_Fetch";
		}
	}
}