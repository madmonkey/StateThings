using StateInterface.Designer.Model;

namespace StateInterface.Areas.Certify.Models
{
    public class RecordsCenterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public RecordsCenterModel()
        {

        }
        public RecordsCenterModel(RecordsCenter recordsCenter)
        {
            Id = recordsCenter.Id;
            Name = recordsCenter.Name;
            Description = recordsCenter.Description;
        }
    }
}