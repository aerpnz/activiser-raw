﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.1433.
// 
#pragma warning disable 1591

namespace activiser.InputGateway.CrmPlugIn.activiserInputGatewayCrm {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="activiserInputGatewayCrmSoap", Namespace="http://activiser.com/")]
    public partial class activiserInputGatewayCrm : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ReadOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetStateOperationCompleted;
        
        private System.Threading.SendOrPostCallback AssignOperationCompleted;
        
        private System.Threading.SendOrPostCallback TestOperationCompleted;
        
        private System.Threading.SendOrPostCallback StartTransactionOperationCompleted;
        
        private System.Threading.SendOrPostCallback FinishTransactionOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateOperationCompleted;
        
        private System.Threading.SendOrPostCallback RouteOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public activiserInputGatewayCrm() {
            this.Url = "http://localhost:4202/activiserInputGatewayCrm.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ReadCompletedEventHandler ReadCompleted;
        
        /// <remarks/>
        public event DeleteCompletedEventHandler DeleteCompleted;
        
        /// <remarks/>
        public event SetStateCompletedEventHandler SetStateCompleted;
        
        /// <remarks/>
        public event AssignCompletedEventHandler AssignCompleted;
        
        /// <remarks/>
        public event TestCompletedEventHandler TestCompleted;
        
        /// <remarks/>
        public event StartTransactionCompletedEventHandler StartTransactionCompleted;
        
        /// <remarks/>
        public event FinishTransactionCompletedEventHandler FinishTransactionCompleted;
        
        /// <remarks/>
        public event CreateCompletedEventHandler CreateCompleted;
        
        /// <remarks/>
        public event RouteCompletedEventHandler RouteCompleted;
        
        /// <remarks/>
        public event UpdateCompletedEventHandler UpdateCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Read", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Read(string transactionId, string organizationId, string userId, string entity, string entityData) {
            object[] results = this.Invoke("Read", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ReadAsync(string transactionId, string organizationId, string userId, string entity, string entityData) {
            this.ReadAsync(transactionId, organizationId, userId, entity, entityData, null);
        }
        
        /// <remarks/>
        public void ReadAsync(string transactionId, string organizationId, string userId, string entity, string entityData, object userState) {
            if ((this.ReadOperationCompleted == null)) {
                this.ReadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReadOperationCompleted);
            }
            this.InvokeAsync("Read", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData}, this.ReadOperationCompleted, userState);
        }
        
        private void OnReadOperationCompleted(object arg) {
            if ((this.ReadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReadCompleted(this, new ReadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Delete", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Delete(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData) {
            object[] results = this.Invoke("Delete", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        entityPreData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData) {
            this.DeleteAsync(transactionId, organizationId, userId, entity, entityData, entityPreData, null);
        }
        
        /// <remarks/>
        public void DeleteAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData, object userState) {
            if ((this.DeleteOperationCompleted == null)) {
                this.DeleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteOperationCompleted);
            }
            this.InvokeAsync("Delete", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        entityPreData}, this.DeleteOperationCompleted, userState);
        }
        
        private void OnDeleteOperationCompleted(object arg) {
            if ((this.DeleteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteCompleted(this, new DeleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/SetState", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SetState(string transactionId, string organizationId, string userId, string entity, string entityData, string newState, int newStatusCode, string entityPreData, string entityPostData) {
            object[] results = this.Invoke("SetState", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        newState,
                        newStatusCode,
                        entityPreData,
                        entityPostData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SetStateAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string newState, int newStatusCode, string entityPreData, string entityPostData) {
            this.SetStateAsync(transactionId, organizationId, userId, entity, entityData, newState, newStatusCode, entityPreData, entityPostData, null);
        }
        
        /// <remarks/>
        public void SetStateAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string newState, int newStatusCode, string entityPreData, string entityPostData, object userState) {
            if ((this.SetStateOperationCompleted == null)) {
                this.SetStateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetStateOperationCompleted);
            }
            this.InvokeAsync("SetState", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        newState,
                        newStatusCode,
                        entityPreData,
                        entityPostData}, this.SetStateOperationCompleted, userState);
        }
        
        private void OnSetStateOperationCompleted(object arg) {
            if ((this.SetStateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetStateCompleted(this, new SetStateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Assign", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Assign(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData) {
            object[] results = this.Invoke("Assign", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        newAssignee,
                        entityPreData,
                        entityPostData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void AssignAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData) {
            this.AssignAsync(transactionId, organizationId, userId, entity, entityData, newAssignee, entityPreData, entityPostData, null);
        }
        
        /// <remarks/>
        public void AssignAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData, object userState) {
            if ((this.AssignOperationCompleted == null)) {
                this.AssignOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAssignOperationCompleted);
            }
            this.InvokeAsync("Assign", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        newAssignee,
                        entityPreData,
                        entityPostData}, this.AssignOperationCompleted, userState);
        }
        
        private void OnAssignOperationCompleted(object arg) {
            if ((this.AssignCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AssignCompleted(this, new AssignCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Test", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Test(string organizationId) {
            object[] results = this.Invoke("Test", new object[] {
                        organizationId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TestAsync(string organizationId) {
            this.TestAsync(organizationId, null);
        }
        
        /// <remarks/>
        public void TestAsync(string organizationId, object userState) {
            if ((this.TestOperationCompleted == null)) {
                this.TestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestOperationCompleted);
            }
            this.InvokeAsync("Test", new object[] {
                        organizationId}, this.TestOperationCompleted, userState);
        }
        
        private void OnTestOperationCompleted(object arg) {
            if ((this.TestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestCompleted(this, new TestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/StartTransaction", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string StartTransaction(string organizationId, string userId) {
            object[] results = this.Invoke("StartTransaction", new object[] {
                        organizationId,
                        userId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void StartTransactionAsync(string organizationId, string userId) {
            this.StartTransactionAsync(organizationId, userId, null);
        }
        
        /// <remarks/>
        public void StartTransactionAsync(string organizationId, string userId, object userState) {
            if ((this.StartTransactionOperationCompleted == null)) {
                this.StartTransactionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnStartTransactionOperationCompleted);
            }
            this.InvokeAsync("StartTransaction", new object[] {
                        organizationId,
                        userId}, this.StartTransactionOperationCompleted, userState);
        }
        
        private void OnStartTransactionOperationCompleted(object arg) {
            if ((this.StartTransactionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.StartTransactionCompleted(this, new StartTransactionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/FinishTransaction", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string FinishTransaction(string organizationId, string transactionId) {
            object[] results = this.Invoke("FinishTransaction", new object[] {
                        organizationId,
                        transactionId});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void FinishTransactionAsync(string organizationId, string transactionId) {
            this.FinishTransactionAsync(organizationId, transactionId, null);
        }
        
        /// <remarks/>
        public void FinishTransactionAsync(string organizationId, string transactionId, object userState) {
            if ((this.FinishTransactionOperationCompleted == null)) {
                this.FinishTransactionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFinishTransactionOperationCompleted);
            }
            this.InvokeAsync("FinishTransaction", new object[] {
                        organizationId,
                        transactionId}, this.FinishTransactionOperationCompleted, userState);
        }
        
        private void OnFinishTransactionOperationCompleted(object arg) {
            if ((this.FinishTransactionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FinishTransactionCompleted(this, new FinishTransactionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Create", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Create(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPostData, bool updateIfExists) {
            object[] results = this.Invoke("Create", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        entityPostData,
                        updateIfExists});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void CreateAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPostData, bool updateIfExists) {
            this.CreateAsync(transactionId, organizationId, userId, entity, entityData, entityPostData, updateIfExists, null);
        }
        
        /// <remarks/>
        public void CreateAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPostData, bool updateIfExists, object userState) {
            if ((this.CreateOperationCompleted == null)) {
                this.CreateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateOperationCompleted);
            }
            this.InvokeAsync("Create", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        entityPostData,
                        updateIfExists}, this.CreateOperationCompleted, userState);
        }
        
        private void OnCreateOperationCompleted(object arg) {
            if ((this.CreateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateCompleted(this, new CreateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Route", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Route(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData) {
            object[] results = this.Invoke("Route", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        newAssignee,
                        entityPreData,
                        entityPostData});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RouteAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData) {
            this.RouteAsync(transactionId, organizationId, userId, entity, entityData, newAssignee, entityPreData, entityPostData, null);
        }
        
        /// <remarks/>
        public void RouteAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string newAssignee, string entityPreData, string entityPostData, object userState) {
            if ((this.RouteOperationCompleted == null)) {
                this.RouteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRouteOperationCompleted);
            }
            this.InvokeAsync("Route", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        newAssignee,
                        entityPreData,
                        entityPostData}, this.RouteOperationCompleted, userState);
        }
        
        private void OnRouteOperationCompleted(object arg) {
            if ((this.RouteCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RouteCompleted(this, new RouteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://activiser.com/Update", RequestNamespace="http://activiser.com/", ResponseNamespace="http://activiser.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Update(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData, string entityPostData, bool createIfMissing) {
            object[] results = this.Invoke("Update", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        entityPreData,
                        entityPostData,
                        createIfMissing});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData, string entityPostData, bool createIfMissing) {
            this.UpdateAsync(transactionId, organizationId, userId, entity, entityData, entityPreData, entityPostData, createIfMissing, null);
        }
        
        /// <remarks/>
        public void UpdateAsync(string transactionId, string organizationId, string userId, string entity, string entityData, string entityPreData, string entityPostData, bool createIfMissing, object userState) {
            if ((this.UpdateOperationCompleted == null)) {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                        transactionId,
                        organizationId,
                        userId,
                        entity,
                        entityData,
                        entityPreData,
                        entityPostData,
                        createIfMissing}, this.UpdateOperationCompleted, userState);
        }
        
        private void OnUpdateOperationCompleted(object arg) {
            if ((this.UpdateCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCompleted(this, new UpdateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void ReadCompletedEventHandler(object sender, ReadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ReadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void DeleteCompletedEventHandler(object sender, DeleteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void SetStateCompletedEventHandler(object sender, SetStateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetStateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetStateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void AssignCompletedEventHandler(object sender, AssignCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AssignCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal AssignCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void TestCompletedEventHandler(object sender, TestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void StartTransactionCompletedEventHandler(object sender, StartTransactionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class StartTransactionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal StartTransactionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void FinishTransactionCompletedEventHandler(object sender, FinishTransactionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FinishTransactionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FinishTransactionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void CreateCompletedEventHandler(object sender, CreateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void RouteCompletedEventHandler(object sender, RouteCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RouteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RouteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    public delegate void UpdateCompletedEventHandler(object sender, UpdateCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.1433")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591