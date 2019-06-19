using System;
using System.Collections.Generic;
using System.Linq;
using InternetShopModel;
using InternetShopServiceDAL.BindingModels;
using InternetShopServiceDAL.Interfaces;
using InternetShopServiceDAL.ViewModel;

namespace InternetShopImplementations.Implementations {
    public class RequestServiceDB : IRequestService {
        private AbstractWebDbContext context;

        public RequestServiceDB() {
            this.context = new AbstractWebDbContext();
        }

        public RequestServiceDB(AbstractWebDbContext context) {
            this.context = context;
        }

        public List<RequestViewModel> GetList() {
            List<RequestViewModel> result = context.Requests
                .Select(rec => new RequestViewModel {
                    Id = rec.Id,
                    RequestName = rec.RequestName,
                    Date = rec.Date,
                    RequestComponents = context.RequestComponents
                        .Where(recPC => recPC.RequestId == rec.Id)
                        .Select(recPC => new RequestComponentViewModel {
                            Id = recPC.Id,
                            ComponentId = recPC.ComponentId,
                            RequestId = recPC.RequestId,
                            ComponentName = recPC.ComponentName,
                            CountComponents = recPC.CountComponents
                        }).ToList()
                }).ToList();
            return result;
        }

        public RequestViewModel GetElement(int id) {
            Request request = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if ( request != null ) {
                return new RequestViewModel {
                    Id = request.Id,
                    RequestName = request.RequestName,
                    Date = request.Date,
                    RequestComponents = context.RequestComponents
                        .Where(recPC => recPC.RequestId == request.Id)
                        .Select(recPC => new RequestComponentViewModel {
                            Id = recPC.Id,
                            ComponentId = recPC.ComponentId,
                            RequestId = recPC.RequestId,
                            ComponentName = recPC.ComponentName,
                            CountComponents = recPC.CountComponents
                        }).ToList()
                };
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(RequestBindingModel model) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Request element = context.Requests.FirstOrDefault(rec =>
                        rec.RequestName == model.RequestName);
                    if ( element != null ) {
                        throw new Exception("Уже есть заявка с таким названием");
                    }

                    element = new Request {
                        RequestName = model.RequestName,
                        Date = model.Date,
                    };
                    context.Requests.Add(element);
                    context.SaveChanges();
                    //// убираем дубли по компонентам 
                    var groupComponents = model.RequestComponents
                        .GroupBy(rec => rec.ComponentID)
                        .Select(rec => new {
                            ComponentId = rec.Key,
                            CountComponents = rec.Sum(r => r.CountComponents)
                        });
                    // добавляем компоненты  
                    foreach ( var groupComponent in groupComponents ) {
                        context.RequestComponents.Add(new RequestComponent {
                            Id = element.Id,
                            ComponentId = groupComponent.ComponentId,
                            CountComponents = groupComponent.CountComponents
                        });
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch ( Exception ) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(RequestBindingModel model) {
            using ( var transaction = context.Database.BeginTransaction() ) {
                try {
                    Request element = context.Requests.FirstOrDefault(rec =>
                        rec.RequestName == model.RequestName &&
                        rec.Id != model.Id);
                    if ( element != null ) {
                        throw new Exception("Уже есть заявка с таким названием");
                    }

                    element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                    if ( element == null ) {
                        throw new Exception("Элемент не найден");
                    }

                    element.RequestName = model.RequestName;
                    element.Date = model.Date;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты 
                    var compIds = model.RequestComponents.Select(rec => rec.ComponentID).Distinct();
                    var updateComponents = context.RequestComponents.Where(rec =>
                        rec.RequestId == model.Id &&
                        compIds.Contains(rec.ComponentId));
                    foreach ( var updateComponent in updateComponents ) {
                        updateComponent.CountComponents = model.RequestComponents.FirstOrDefault(rec =>
                                rec.Id ==
                                updateComponent.Id)
                            .CountComponents;
                    }

                    context.SaveChanges();
                    context.RequestComponents.RemoveRange(context.RequestComponents.Where(rec =>
                        rec.RequestId ==
                        model.Id && !compIds
                            .Contains(rec
                                .ComponentId)));
                    context.SaveChanges();
                    // новые записи  
                    var groupComponents = model.RequestComponents.Where(rec =>
                            rec.Id == 0).GroupBy(rec => rec.ComponentID)
                        .Select(rec => new {
                            ComponentId = rec.Key,
                            CountComponents = rec.Sum(r => r.CountComponents)
                        });
                    foreach ( var groupComponent in groupComponents ) {
                        RequestComponent elementPC = context.RequestComponents.FirstOrDefault(rec =>
                            rec.RequestId ==
                            model.Id &&
                            rec.ComponentId ==
                            groupComponent
                                .ComponentId);
                        if ( elementPC != null ) {
                            elementPC.CountComponents += groupComponent.CountComponents;
                            context.SaveChanges();
                        }
                        else {
                            context.RequestComponents.Add(new RequestComponent {
                                RequestId = model.Id,
                                ComponentId = groupComponent.ComponentId,
                                CountComponents = groupComponent.CountComponents
                            });
                            context.SaveChanges();
                        }
                    }

                    transaction.Commit();
                }
                catch ( Exception ) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id) {
            Request request = context.Requests.FirstOrDefault(rec => rec.Id == id);
            if ( request != null ) {
                context.Requests.Remove(request);
                context.SaveChanges();
            }
            else {
                throw new Exception("Элемент не найден");
            }
        }
    }
}