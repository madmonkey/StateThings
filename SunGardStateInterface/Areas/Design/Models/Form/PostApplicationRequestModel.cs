﻿using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateInterface.Areas.Design.Models
{
    public class PostApplicationRequestModel
    {
        public string RecordsCenterName { get; set; }
        public string FormId { get; set; }
        public List<SelectItemModel> Applications { get; set; }

        public PostApplicationRequestModel()
        {
            Applications = new List<SelectItemModel>();
        }
        public PostApplicationRequestModel(RequestForm applicationFormProjection, IEnumerable<Application> availableApplications)
        {
            Applications = new List<SelectItemModel>();
            this.RecordsCenterName = applicationFormProjection.RecordsCenter.Name;
            this.FormId = applicationFormProjection.FormId;
            foreach (var application in availableApplications)
            {
                this.Applications.Add(new SelectItemModel()
                {
                    Id = application.Id,
                    Name = application.Name,
                    Description = application.Description,
                    IsSelected = applicationFormProjection.Applications.Any(x => x.Id == application.Id)
                });
            }
        }
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(FormId))
            {
                throw new StateInterfaceParameterValidationException("Invalid FormId");
            }

            if (string.IsNullOrEmpty(RecordsCenterName))
            {
                throw new StateInterfaceParameterValidationException("Invalid RecordCenter");
            }

            if (Applications == null)
            {
                throw new StateInterfaceParameterValidationException("Invalid Application Association");
            }
        }
    }
}