<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="d0ed9acb-0435-4532-afdd-b5115bc4d562" namespace="CN100.EnterprisePlatform.Configuration" xmlSchemaNamespace="CN100.EnterprisePlatform.Configuration" assemblyName="CN100.EnterprisePlatform.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="DomainSection" isReadOnly="true" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/domainSection">
      <elementProperties>
        <elementProperty name="Urls" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="urls" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/DomainElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationSectionGroup name="CN100SectionGroup">
      <configurationSectionProperties>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/DomainSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ImageSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MqSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WcfSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/StyleSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/EmailSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
        <configurationSectionProperty>
          <containedConfigurationSection>
            <configurationSectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/BaseSection" />
          </containedConfigurationSection>
        </configurationSectionProperty>
      </configurationSectionProperties>
    </configurationSectionGroup>
    <configurationElementCollection name="DomainElementCollection" xmlItemName="url" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/DomainElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="DomainElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Url" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="url" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="WcfSection" isReadOnly="true" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/wcfSection">
      <elementProperties>
        <elementProperty name="Hosts" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="hosts" isReadOnly="false" documentation="wcf service address">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WcfElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationSection name="ImageSection" isReadOnly="true" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/imageSection">
      <elementProperties>
        <elementProperty name="Urls" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="urls" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ImageElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationSection name="MqSection" isReadOnly="true" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/mqSection">
      <elementProperties>
        <elementProperty name="Hosts" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="hosts" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MqElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="WcfElementCollection" xmlItemName="items" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WcfItemsElementCollection" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="WcfElement">
      <attributeProperties>
        <attributeProperty name="Ip" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="ip" isReadOnly="false" documentation="Service IP address" defaultValue="&quot;127.0.0.1&quot;">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false" documentation="Service port number">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="IsDefault" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isDefault" isReadOnly="false" defaultValue="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Boolean" />
          </type>
        </attributeProperty>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="ImageElementCollection" xmlItemName="url" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/ImageElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ImageElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Url" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="url" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="MqElementCollection" xmlItemName="host" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/MqElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="MqElement">
      <attributeProperties>
        <attributeProperty name="ServiceName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="serviceName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Ip" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="ip" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="StyleSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/styleSection">
      <elementProperties>
        <elementProperty name="Urls" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="urls" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/StyleElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="StyleElementCollection" xmlItemName="url" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/StyleElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="StyleElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Url" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="url" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="EmailSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/emailSection">
      <elementProperties>
        <elementProperty name="Hosts" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="hosts" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/EmailElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="EmailElementCollection" xmlItemName="host" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/EmailElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="EmailElement">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="true" documentation="The mail server address">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Address" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="address" isReadOnly="true" documentation="Mailing address">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Account" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="account" isReadOnly="true" documentation="Mail server login">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Password" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="password" isReadOnly="true" documentation="Mail server login password">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="BaseSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="CN100SectionGroup/baseSection">
      <elementProperties>
        <elementProperty name="Base" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="base" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/BaseElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="BaseElementCollection" collectionType="AddRemoveClearMapAlternate" xmlItemName="baseElement" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/BaseElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="BaseElement">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Value" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="value" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="WcfItemsElementCollection" xmlItemName="item" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/d0ed9acb-0435-4532-afdd-b5115bc4d562/WcfElement" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>