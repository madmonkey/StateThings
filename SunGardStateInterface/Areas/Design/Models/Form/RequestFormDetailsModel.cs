﻿using System.Threading;
using StateInterface.Designer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StateInterface.Properties;

namespace StateInterface.Areas.Design.Models
{
    public class RequestFormDetailsModel
    {
        public int Id { get; set; }
        public string RecordsCenterName { get; set; }
        public string Version { get; set; }
        public string LastUpdated { get; set; }
        public string FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<FormFieldModel> FormFields { get; set; }
        public List<SelectItemModel> Applications { get; set; }
        public List<SelectItemModel> ApplicationsForEdit { get; set; }
        public List<SelectItemModel> Categories { get; set; }
        public List<SelectItemModel> CategoriesForEdit { get; set; }
        public string SubmissionMode { get; set; }
        public List<TransactionModel> Transactions { get; set; }
        public string InitialData { get; set; }
        public string FormHelpUrl { get; set; }
        public string PreviewFormUrl { get; set; }
        public bool CanDesignManage { get; set; }
        public string UpdateApplicationsAssociationUrl { get; set; }
        public string UpdateCategoriesAssociationUrl { get; set; }
        public string DesignHomeUrl { get; set; }
        public string FormsHomeUrl { get; set; }

        public RequestFormDetailsModel(RequestForm requestForm, string listDetailsUrl, string fieldDetailsUrl,
            IEnumerable<Application> availableApplications, IEnumerable<Category> availableCategories)
            : this(requestForm, listDetailsUrl, fieldDetailsUrl)
        {
            var selectedApplications = this.Applications.ToList();
            this.Applications = new List<SelectItemModel>();
            foreach (var application in availableApplications)
            {
                this.Applications.Add(new SelectItemModel() 
                {
                    Id = application.Id,
                    Name = application.Name,
                    Description = application.Description,
                    IsSelected = selectedApplications.Any(x => x.Id == application.Id) 
                });
            }

            var selectedCategories = this.Categories.ToList();
            this.Categories = new List<SelectItemModel>();
            foreach (var category in availableCategories)
            {
                this.Categories.Add(new SelectItemModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    IsSelected = selectedCategories.Any(x => x.Id == category.Id)
                });
            }
        }
        public RequestFormDetailsModel(RequestForm requestForm, string listDetailsUrl, string fieldDetailsUrl)
        {
            Id = requestForm.Id;
            RecordsCenterName = requestForm.RecordsCenter.Name;

            Version = requestForm.Version;

            LastUpdated = requestForm.Updated.HasValue
                ? requestForm.Updated.Value.ToString(Resources.DateTimeFormat)
                : requestForm.Created.ToString(Resources.DateTimeFormat);

            FormId = requestForm.FormId;
            Title = requestForm.Title;
            Description = requestForm.Description;

            listDetailsUrl = string.Format("{0}/{1}", listDetailsUrl, requestForm.RecordsCenter.Name);
            fieldDetailsUrl = string.Format("{0}/{1}", fieldDetailsUrl, requestForm.RecordsCenter.Name);

            FormFields = new List<FormFieldModel>();
            foreach (FormField formField in requestForm.FormFields.OrderBy(x => x.Sequence))
            {
                FormFields.Add(new FormFieldModel(formField, listDetailsUrl, fieldDetailsUrl));
            }

            Categories = new List<SelectItemModel>();
            CategoriesForEdit = new List<SelectItemModel>();
            foreach (Category category in requestForm.Categories.OrderBy(x => x.Name))
            {
                Categories.Add(new SelectItemModel()
                    {
                        Id = category.Id,
                        Name = category.Name,
                    });
            }

            Applications = new List<SelectItemModel>();
            ApplicationsForEdit = new List<SelectItemModel>();
            foreach (Application application in requestForm.Applications.OrderBy(x => x.Name))
            {
                Applications.Add(new SelectItemModel()
                    {
                        Id = application.Id,
                        Name = application.Name,
                        Description = application.Description,
                    });
            }

            SubmissionMode = requestForm.SubmissionMode.ToString();

            Transactions = new List<TransactionModel>();
            foreach (var transaction in requestForm.Transactions.OrderBy(x => x.Sequence))
            {
                var transactionViewModel = new TransactionModel(transaction);

                Transactions.Add(transactionViewModel);
            }
        }
    }
}