using SQLite;

namespace TonkostiLocationParser.Db
{
	[Table("locations")]
	public class LocationDataObject
	{
		[Column("id")]
		[PrimaryKeyAttribute]
		[AutoIncrementAttribute]
		public int id { get; set; }

		[Column("parent_id")]
		public int? parent_id { get; set; }

		[Column("country_id")]
		public int country_id { get; set; }

		[Column("name")]
		public string name { get; set; }

		[Column("level")]
		public int level { get; set; }

		[Column("is_capital")]
		public int is_capital { get; set; }
	}
}
