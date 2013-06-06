<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="d6262a1a-1be8-4461-9113-362b2a9a2b9e" namespace="CN100.WcfService" xmlSchemaNamespace="CN100.WcfService" assemblyName="CN100.WcfService" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="WcfServiceSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="wcfServiceSection">
      <attributeProperties>
        <attributeProperty name="Version" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="version" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="ServiceName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="serviceName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="Host" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/WcfServiceElement" />
          </type>
        </elementProperty>
        <elementProperty name="Items" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="items" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/InterfaceElementColletion" />
          </type>
        </elementProperty>
        <elementProperty name="Services" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="services" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/ServiceElementCollection" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="WcfServiceElement">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false" documentation="&quot;Service Name&quot;" displayName="&quot;ServiceName&quot;">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="IP" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="iP" isReadOnly="false" documentation="&quot;IP&quot;">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="Port" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="port" isReadOnly="false" documentation="&quot;Port number&quot;" displayName="&quot;Port&quot;">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElement name="DllElement">
      <attributeProperties>
        <attributeProperty name="Key" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="key" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="DllName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="dllName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationElementCollection name="InterfaceElementColletion" xmlItemName="interface" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/DllElement" />
      </itemType>
    </configurationElementCollection>
    <configurationElementCollection name="ServiceElementCollection" xmlItemName="service" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <itemType>
        <configurationElementMoniker name="/d6262a1a-1be8-4461-9113-362b2a9a2b9e/DllElement" />
      </itemType>
    </configurationElementCollection>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>