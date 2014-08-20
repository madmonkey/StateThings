using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Designer.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateInterface.Designer.Model;
using StateInterface.Designer.Repository;
using StateInterface.Designer.Model.Projections;

namespace StateInterface.Test
{
    [TestClass]
    public class StatisticsTest
    {
        [TestMethod]
        public void GetStuff()
        {
            IDesignerTasks repository = new DesignerTasks(new DesignerRepository());
            var stuff = repository.GetRecordsCenterAcceptanceStatus(8);

            Console.WriteLine(stuff.Count());
            //IStateInterfaceRepository theOtherRepository = new StateInterfaceRepository();
            //var rc = repository.GetRecordsCenters().FirstOrDefault(x => x.Id == 8);
            //theOtherRepository.
            //stateInterfaceRepository.
        }

        [TestMethod]
        public void Initialize()
        {
           IDesignerTasks repository = new DesignerTasks(new DesignerRepository());
           //var test = repository.GetFormsByApplication(1);
           //Console.WriteLine(test.Count());
           var test2 = repository.GetApplications();
            
        }
        [TestMethod]
        public void Save()
        {
            var repository = new DesignerRepository();
            var application = repository.GetById<Application>(1);
            var requestForms = repository.GetAll<ApplicationFormProjection>();
            foreach (var requestForm in requestForms)
            {
                requestForm.Applications.Add(application);
                repository.Save(requestForm);
                //application.RequestForms.Add(requestForm);
                //requestForm.Applications.Add(application);
                //repository.Save(requestForm);
            }
            //repository.Save(application);
            //repository.Remove<Application>(application);


        }
        [TestMethod]
        public void Transactions()
        {
            var descriptionbucket = new List<string>(){
                "The best description", "the worst description", "shoot me now","wait until you get home","lorem ipsum"
            };
            var tokenbucket = new List<string>(){
                "ABCF", "MMGRT", "BLAH", "WUT", "OKAY"
            };
            var repository = new DesignerRepository();
            var rcs = repository.GetAll<RecordsCenter>();
            foreach (var rc in rcs)
            {
                for (int i = 0; i < new Random().Next( 0, 4 ); i++)
                {
                    var ts = new TransactionSnippet()
                    {
                        Description = descriptionbucket[new Random().Next( 0, 4 )],
                        TokenName = tokenbucket[new Random().Next( 0, 4 )]
                    };
                    //for (int y = 0; y < new Random().Next( 0, 4 ); y++)
                    //{
                    //    ts.TransactionSnippetFields.Add( new TransactionSnippetField());
                    //}
                    repository.Save(ts);
                }
                
            }
            
        }
    }
}
