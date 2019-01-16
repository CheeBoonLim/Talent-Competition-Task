using Talent.Data;
using Talent.Service.Domain;
using Talent.Service.Utilities;
using Talent.Service.Validation;
using Talent.Service.Validation.ModelValidation;
using Talent.WebApp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Talent.WebApp.Controllers
{
    public abstract class MultiModelDomainController<TIdType, TEntity, TListModel, TCreateModel, TEditModel> : BaseController<TEntity, TListModel, TCreateModel, TEditModel>
        where TEntity : class, new()
        where TListModel : new()
        where TCreateModel : new()
        where TEditModel : new()
    {
        #region Lifetime
        protected MultiModelDomainController(
                        IApplicationContext applicationContext,
                        IRepository<TEntity> domainService,
                        IValidationService<TEntity> validationService,
                        IModelValidationService<TCreateModel, TEditModel> modelValidationService)
            : base(applicationContext, domainService, validationService, modelValidationService)
        {
        }
        #endregion

        #region Abstract Methods
        protected abstract TEntity GetEntity(TIdType id);

        protected virtual bool HasAccess(TIdType id)
        {
            return false;
        }
        #endregion

        #region Actions
        [HttpGet]
        public virtual ActionResult Index()
        {
            //var viewModel = GetListEntities().MapEnumerable<TEntity, TListModel>(MapEntityToListModel);
            //return View(viewModel);

            return View();
        }

        [HttpGet]
        [AuthorizationRequireWrite]
        public virtual ActionResult Create()
        {
            return GetView(PrepareCreateModel());
        }

        [HttpPost]
        [AuthorizationRequireWrite]
        public virtual ActionResult Create(TCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                // Send the errors to the debug console.
                ModelState.OutputErrors();

                PopulateCreateModel(createModel);
                return CreateFailed(createModel);
            }

            var result = ProcessCreate(createModel);

            if (result.Errors.Any())
            {
                ModelState.AddModelErrors(result.Errors);
                PopulateCreateModel(createModel);
                return CreateFailed(createModel);
            }

            return CreateSucceeded(createModel, result.Entity);
        }

        [HttpGet]
        [AuthorizationRequireWrite]
        public virtual ActionResult Detail(TIdType id)
        {
            var entity = GetEntity(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = new TEditModel();
            MapEntityToDetailModel(entity, model);
            return GetView(model);
        }

        [HttpGet]
        [AuthorizationRequireWrite]
        public virtual ActionResult Edit(TIdType id)
        {
            var entity = GetEntity(id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            var model = PrepareEditModel(entity);
            return GetView(model);
        }

        [HttpPost]
        [AuthorizationRequireWrite]
        public virtual ActionResult Edit(TIdType id, TEditModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Send the errors to the debug console.
                ModelState.OutputErrors();

                PopulateEditModel(viewModel);
                return EditFailed(viewModel);
            }

            var entity = GetEntity(id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            var errors = ProcessEdit(entity, viewModel);
            if (errors.Any())
            {
                ModelState.AddModelErrors(errors);
                PopulateEditModel(viewModel);

                return EditFailed(viewModel);
            }

            return EditSucceeded(viewModel, entity);
        }

        [HttpGet]
        [ActionName("Delete")]
        [AuthorizationRequireWrite]
        public virtual ActionResult DeleteGet(TIdType id)
        {
            var entity = GetEntity(id);
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            //return PartialView(PrepareDeleteModel(entity));
            return View(PrepareDeleteModel(entity));
        }

        [HttpPost]
        [AuthorizationRequireWrite]
        public virtual ActionResult Delete(TIdType id)
        {
            var entity = GetEntity(id);
            if (entity == null)
            {
                return HttpNotFound();
            }

            ProcessDelete(entity);

            return DeleteSucceeded(entity);
        }
        #endregion
    }
}