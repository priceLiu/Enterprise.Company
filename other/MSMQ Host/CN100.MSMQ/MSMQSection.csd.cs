//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CN100.MSMQ
{
    
    
    /// <summary>
    /// The MSMQSection Configuration Section.
    /// </summary>
    public partial class MSMQSection : global::System.Configuration.ConfigurationSection
    {
        
        #region Singleton Instance
        /// <summary>
        /// The XML name of the MSMQSection Configuration Section.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string MSMQSectionSectionName = "MSMQSection";
        
        /// <summary>
        /// Gets the MSMQSection instance.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public static global::CN100.MSMQ.MSMQSection Instance
        {
            get
            {
                return ((global::CN100.MSMQ.MSMQSection)(global::System.Configuration.ConfigurationManager.GetSection(global::CN100.MSMQ.MSMQSection.MSMQSectionSectionName)));
            }
        }
        #endregion
        
        #region Xmlns Property
        /// <summary>
        /// The XML name of the <see cref="Xmlns"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string XmlnsPropertyName = "xmlns";
        
        /// <summary>
        /// Gets the XML namespace of this Configuration Section.
        /// </summary>
        /// <remarks>
        /// This property makes sure that if the configuration file contains the XML namespace,
        /// the parser doesn't throw an exception because it encounters the unknown "xmlns" attribute.
        /// </remarks>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.MSMQSection.XmlnsPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public string Xmlns
        {
            get
            {
                return ((string)(base[global::CN100.MSMQ.MSMQSection.XmlnsPropertyName]));
            }
        }
        #endregion
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region Connections Property
        /// <summary>
        /// The XML name of the <see cref="Connections"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string ConnectionsPropertyName = "Connections";
        
        /// <summary>
        /// Gets or sets the Connections.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The Connections.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.MSMQSection.ConnectionsPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public virtual global::CN100.MSMQ.add Connections
        {
            get
            {
                return ((global::CN100.MSMQ.add)(base[global::CN100.MSMQ.MSMQSection.ConnectionsPropertyName]));
            }
            set
            {
                base[global::CN100.MSMQ.MSMQSection.ConnectionsPropertyName] = value;
            }
        }
        #endregion
    }
}
namespace CN100.MSMQ
{
    
    
    /// <summary>
    /// A collection of ItemConfig instances.
    /// </summary>
    [global::System.Configuration.ConfigurationCollectionAttribute(typeof(global::CN100.MSMQ.ItemConfig), CollectionType=global::System.Configuration.ConfigurationElementCollectionType.BasicMapAlternate, AddItemName=global::CN100.MSMQ.add.ItemConfigPropertyName)]
    public partial class add : global::System.Configuration.ConfigurationElementCollection
    {
        
        #region Constants
        /// <summary>
        /// The XML name of the individual <see cref="global::CN100.MSMQ.ItemConfig"/> instances in this collection.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string ItemConfigPropertyName = "add";
        #endregion
        
        #region Overrides
        /// <summary>
        /// Gets the type of the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <returns>The <see cref="global::System.Configuration.ConfigurationElementCollectionType"/> of this collection.</returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override global::System.Configuration.ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return global::System.Configuration.ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        
        /// <summary>
        /// Gets the name used to identify this collection of elements
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override string ElementName
        {
            get
            {
                return global::CN100.MSMQ.add.ItemConfigPropertyName;
            }
        }
        
        /// <summary>
        /// Indicates whether the specified <see cref="global::System.Configuration.ConfigurationElement"/> exists in the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="elementName">The name of the element to verify.</param>
        /// <returns>
        /// <see langword="true"/> if the element exists in the collection; otherwise, <see langword="false"/>.
        /// </returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override bool IsElementName(string elementName)
        {
            return (elementName == global::CN100.MSMQ.add.ItemConfigPropertyName);
        }
        
        /// <summary>
        /// Gets the element key for the specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="global::System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="object"/> that acts as the key for the specified <see cref="global::System.Configuration.ConfigurationElement"/>.
        /// </returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override object GetElementKey(global::System.Configuration.ConfigurationElement element)
        {
            return ((global::CN100.MSMQ.ItemConfig)(element)).connectionName;
        }
        
        /// <summary>
        /// Creates a new <see cref="global::CN100.MSMQ.ItemConfig"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="global::CN100.MSMQ.ItemConfig"/>.
        /// </returns>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        protected override global::System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new global::CN100.MSMQ.ItemConfig();
        }
        #endregion
        
        #region Indexer
        /// <summary>
        /// Gets the <see cref="global::CN100.MSMQ.ItemConfig"/> at the specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="global::CN100.MSMQ.ItemConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::CN100.MSMQ.ItemConfig this[int index]
        {
            get
            {
                return ((global::CN100.MSMQ.ItemConfig)(base.BaseGet(index)));
            }
        }
        
        /// <summary>
        /// Gets the <see cref="global::CN100.MSMQ.ItemConfig"/> with the specified key.
        /// </summary>
        /// <param name="connectionName">The key of the <see cref="global::CN100.MSMQ.ItemConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::CN100.MSMQ.ItemConfig this[object connectionName]
        {
            get
            {
                return ((global::CN100.MSMQ.ItemConfig)(base.BaseGet(connectionName)));
            }
        }
        #endregion
        
        #region Add
        /// <summary>
        /// Adds the specified <see cref="global::CN100.MSMQ.ItemConfig"/> to the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="add">The <see cref="global::CN100.MSMQ.ItemConfig"/> to add.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public void Add(global::CN100.MSMQ.ItemConfig add)
        {
            base.BaseAdd(add);
        }
        #endregion
        
        #region Remove
        /// <summary>
        /// Removes the specified <see cref="global::CN100.MSMQ.ItemConfig"/> from the <see cref="global::System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="add">The <see cref="global::CN100.MSMQ.ItemConfig"/> to remove.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public void Remove(global::CN100.MSMQ.ItemConfig add)
        {
            base.BaseRemove(this.GetElementKey(add));
        }
        #endregion
        
        #region GetItem
        /// <summary>
        /// Gets the <see cref="global::CN100.MSMQ.ItemConfig"/> at the specified index.
        /// </summary>
        /// <param name="index">The index of the <see cref="global::CN100.MSMQ.ItemConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::CN100.MSMQ.ItemConfig GetItemAt(int index)
        {
            return ((global::CN100.MSMQ.ItemConfig)(base.BaseGet(index)));
        }
        
        /// <summary>
        /// Gets the <see cref="global::CN100.MSMQ.ItemConfig"/> with the specified key.
        /// </summary>
        /// <param name="connectionName">The key of the <see cref="global::CN100.MSMQ.ItemConfig"/> to retrieve.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public global::CN100.MSMQ.ItemConfig GetItemByKey(string connectionName)
        {
            return ((global::CN100.MSMQ.ItemConfig)(base.BaseGet(((object)(connectionName)))));
        }
        #endregion
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region MaxThreads Property
        /// <summary>
        /// The XML name of the <see cref="MaxThreads"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string MaxThreadsPropertyName = "MaxThreads";
        
        /// <summary>
        /// Gets or sets the MaxThreads.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The MaxThreads.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.add.MaxThreadsPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public virtual int MaxThreads
        {
            get
            {
                return ((int)(base[global::CN100.MSMQ.add.MaxThreadsPropertyName]));
            }
            set
            {
                base[global::CN100.MSMQ.add.MaxThreadsPropertyName] = value;
            }
        }
        #endregion
    }
}
namespace CN100.MSMQ
{
    
    
    /// <summary>
    /// The ItemConfig Configuration Element.
    /// </summary>
    public partial class ItemConfig : global::System.Configuration.ConfigurationElement
    {
        
        #region IsReadOnly override
        /// <summary>
        /// Gets a value indicating whether the element is read-only.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        public override bool IsReadOnly()
        {
            return false;
        }
        #endregion
        
        #region connectionName Property
        /// <summary>
        /// The XML name of the <see cref="connectionName"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string connectionNamePropertyName = "connectionName";
        
        /// <summary>
        /// Gets or sets the connectionName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The connectionName.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.ItemConfig.connectionNamePropertyName, IsRequired=true, IsKey=true, IsDefaultCollection=false)]
        public virtual string connectionName
        {
            get
            {
                return ((string)(base[global::CN100.MSMQ.ItemConfig.connectionNamePropertyName]));
            }
            set
            {
                base[global::CN100.MSMQ.ItemConfig.connectionNamePropertyName] = value;
            }
        }
        #endregion
        
        #region host Property
        /// <summary>
        /// The XML name of the <see cref="host"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string hostPropertyName = "host";
        
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The host.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.ItemConfig.hostPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public virtual string host
        {
            get
            {
                return ((string)(base[global::CN100.MSMQ.ItemConfig.hostPropertyName]));
            }
            set
            {
                base[global::CN100.MSMQ.ItemConfig.hostPropertyName] = value;
            }
        }
        #endregion
        
        #region queueName Property
        /// <summary>
        /// The XML name of the <see cref="queueName"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string queueNamePropertyName = "queueName";
        
        /// <summary>
        /// Gets or sets the queueName.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The queueName.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.ItemConfig.queueNamePropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public virtual string queueName
        {
            get
            {
                return ((string)(base[global::CN100.MSMQ.ItemConfig.queueNamePropertyName]));
            }
            set
            {
                base[global::CN100.MSMQ.ItemConfig.queueNamePropertyName] = value;
            }
        }
        #endregion
        
        #region isTransactional Property
        /// <summary>
        /// The XML name of the <see cref="isTransactional"/> property.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        internal const string isTransactionalPropertyName = "isTransactional";
        
        /// <summary>
        /// Gets or sets the isTransactional.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("ConfigurationSectionDesigner.CsdFileGenerator", "2.0.0.5")]
        [global::System.ComponentModel.DescriptionAttribute("The isTransactional.")]
        [global::System.Configuration.ConfigurationPropertyAttribute(global::CN100.MSMQ.ItemConfig.isTransactionalPropertyName, IsRequired=false, IsKey=false, IsDefaultCollection=false)]
        public virtual bool isTransactional
        {
            get
            {
                return ((bool)(base[global::CN100.MSMQ.ItemConfig.isTransactionalPropertyName]));
            }
            set
            {
                base[global::CN100.MSMQ.ItemConfig.isTransactionalPropertyName] = value;
            }
        }
        #endregion
    }
}
