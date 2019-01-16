using log4net;
using Omu.ValueInjecter;
using Omu.ValueInjecter.Injections;
using Talent.Data;
using Talent.Service.Domain;
using Talent.Core;
using Talent.Service.Validation;
using Talent.Service.Validation.ModelValidation;
using Talent.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Talent.Diagnostics;

namespace Talent.WebApp.Controllers
{
    public abstract class BaseController<TEntity, TListModel, TCreateModel, TEditModel> : Controller
         where TEntity : class, new()
         where TListModel : new()
         where TCreateModel : new()
         where TEditModel : new()
    {
        #region Data Members

        protected readonly IApplicationContext _applicationContext;
        protected readonly IRepository<TEntity> _repository;
        protected readonly IValidationService<TEntity> _validationService;
        protected readonly IModelValidationService<TCreateModel, TEditModel> _modelValidationService;

        #endregion

        #region Lifetime
        protected BaseController(IApplicationContext applicationContext,
                                IRepository<TEntity> repository,
                                IValidationService<TEntity> validationService,
                                IModelValidationService<TCreateModel, TEditModel> modelValidationService
            // Func<object, ILogService> logServiceFactory
            )
        {
            _applicationContext = applicationContext;
            _repository = repository;
            _validationService = validationService;
            _modelValidationService = modelValidationService;
            // _logService = logServiceFactory(this);
        }
        #endregion

        #region Process Flow Virtuals
        protected virtual void ExecutePostCreateSaveActions(TCreateModel createModel, TEntity entity)
        {
        }

        protected virtual void ExecutePostEditSaveActions(TEditModel editModel, TEntity entity)
        {
        }

        protected virtual void ExecutePostUnitOfWorkEntityDeleteActions(TEntity entity)
        {
        }

        protected virtual TCreateModel PrepareCreateModel()
        {
            var model = new TCreateModel();
            DefaultCreateModel(model);
            PopulateCreateModel(model);
            return model;
        }

        protected virtual void DefaultCreateModel(TCreateModel createModel)
        {
        }

        protected virtual void PopulateCreateModel(TCreateModel createModel)
        {
        }

        protected virtual void MapCreateModelToEntity(TCreateModel createModel, TEntity entity)
        {
            entity.InjectFrom(new LoopInjection(new[] { "Id" }), createModel);
        }

        protected virtual ProcessCreateResult ProcessCreate(TCreateModel createModel)
        {
            var result = new ProcessCreateResult();

            result.Errors = _modelValidationService.ValidateCreateModel(createModel).ToList();

            if (result.Errors.Any())
                return result;

            var entity = _repository.Create();
            MapCreateModelToEntity(createModel, entity);

            result.Errors = _validationService.Validate(EntityMode.Create, entity);

            if (!result.Errors.Any())
            {
                try
                {
                    _repository.Add(entity);
                    ExecutePostCreateSaveActions(createModel, entity);
                    result.Entity = entity;
                    //log.Error("BaseController:Create caught exception on save");
                }
                catch (Exception ex)
                {
                    // _logService.Error("BaseController:Create caught exception on save", ex);
                    Logging.Error("BaseController:Create caught exception on save", ex);
                    throw;
                }
            }
            return result;
        }

        protected virtual TEditModel PrepareEditModel(TEntity entity)
        {
            var model = new TEditModel();
            PopulateEditModel(model);
            MapEntityToEditModel(entity, model);
            return model;
        }

        protected virtual void PopulateEditModel(TEditModel editModel)
        {
        }

        protected virtual void MapEditModelToEntity(TEditModel editModel, TEntity entity)
        {
            entity.InjectFrom(new LoopInjection(new[] { "Id", "UID" }), editModel);
        }

        protected virtual void MapEntityToEditModel(TEntity entity, TEditModel editModel)
        {
            editModel.InjectFrom(entity);
        }

        protected virtual void MapEntityToDetailModel(TEntity entity, TEditModel detailModel)
        {
            detailModel.InjectFrom(entity);
        }

        protected virtual void MapEntityToDeleteModel(TEntity entity, TEditModel deleteModel)
        {
            deleteModel.InjectFrom(entity);
        }

        protected virtual List<ValidationResult> ProcessEdit(TEntity entity, TEditModel editModel)
        {
            var errors = _modelValidationService.ValidateEditModel(editModel);
            if (errors.Any())
                return errors.ToList();

            MapEditModelToEntity(editModel, entity);

            errors = _validationService.Validate(EntityMode.Edit, entity);
            if (!errors.Any())
            {
                _repository.Update(entity);

                ExecutePostEditSaveActions(editModel, entity);
            }
            return errors.ToList();
        }

        protected virtual TEditModel PrepareDeleteModel(TEntity entity)
        {
            var model = new TEditModel();
            MapEntityToDeleteModel(entity, model);
            return model;
        }

        protected virtual void ProcessDelete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        protected virtual void DefaultCopyModel(TEditModel copyModel)
        {
        }

        protected virtual void MapEntityToListModel(TEntity entity, TListModel listModel)
        {
        }

        protected virtual IEnumerable<TEntity> GetListEntities()
        {
            return _repository.GetQueryable();
        }
        #endregion

        #region Helper Methods
        protected virtual ActionResult GetView(TEditModel editModel)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(editModel);
            }

            return View(editModel);
        }

        protected virtual ActionResult GetView(TCreateModel createModel)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(createModel);
            }

            return View(createModel);
        }
        #endregion

        #region Action Results
        protected virtual ActionResult EditSucceeded(TEditModel editModel, TEntity entity)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new ExtendedJsonResult<TEditModel>
            //    {
            //        IsSuccess = true,
            //        EntityId = entity.Id,
            //        Operation = OperationType.Edit.ToString()
            //    };
            //}
            return RedirectToAction("Index");
        }

        protected virtual ActionResult DeleteSucceeded(TEntity entity = null)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new ExtendedJsonResult<TCreateModel>
            //    {
            //        IsSuccess = true,
            //        EntityId = entity != null ? entity.Id : 0,
            //        Operation = OperationType.Delete.ToString()
            //    };
            //}
            return RedirectToAction("Index");
        }

        protected virtual ActionResult DeleteFailed(TEntity entity = null)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new ExtendedJsonResult<TCreateModel>
            //    {
            //        IsSuccess = false,
            //        EntityId = entity != null ? entity.Id : 0,
            //        Operation = OperationType.Delete.ToString()
            //    };
            //}
            return RedirectToAction("Index");
        }

        protected virtual ActionResult EditFailed(TEditModel editModel)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new ExtendedJsonResult<TEditModel>
            //    {
            //        IsSuccess = false,
            //        Operation = OperationType.Edit.ToString(),
            //        Views = new[] { this.RenderPartialViewToString("Edit", editModel) }
            //    };
            //}

            return View(editModel);
        }

        protected virtual ActionResult CreateSucceeded(TCreateModel createModel, TEntity entity)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new ExtendedJsonResult<TCreateModel>
            //    {
            //        IsSuccess = true,
            //        EntityId = entity.Id,
            //        Operation = OperationType.Create.ToString(),
            //        Views = new[] { this.RenderPartialViewToString("Create", createModel, false) }
            //    };
            //}
            TempData["SaveMsg"] = new object[] { true, "Add successfully" };
            //TempData["SaveMsg"] = "Add successfully";
            return RedirectToAction("Index");
        }

        protected virtual ActionResult CreateFailed(TCreateModel createModel)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    return new ExtendedJsonResult<TCreateModel>
            //    {
            //        IsSuccess = false,
            //        Operation = OperationType.Create.ToString(),
            //        Views = new[] { this.RenderPartialViewToString("Create", createModel) }
            //    };
            //}
            TempData["SaveMsg"] = new object[] { true, "Add unsuccessfully" };
            return View(createModel);
        }

        protected class ProcessCreateResult
        {
            public ProcessCreateResult()
            {
                Errors = new List<ValidationResult>();
            }

            public List<ValidationResult> Errors { get; set; }
            public TEntity Entity { get; set; }

        }

        protected virtual ActionResult JsonSucceeded(string message)
        {
            return Json(new
            {
                IsSuccess = true,
                Message = message
            });
        }

        protected virtual ActionResult JsonFailed(string message)
        {
            return Json(new
            {
                IsSuccess = false,
                Message = message
            });
        }
        #endregion
    }
}