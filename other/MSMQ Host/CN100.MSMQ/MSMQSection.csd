<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="103f9a40-1c8d-47a0-9ae5-770437a1b9ce" namespace="CN100.MSMQ" xmlSchemaNamespace="urn:CN100.MSMQ" assemblyName="CN100.MSMQ" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
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
    <configurationSection name="MSMQSection" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="MSMQSection">
      <elementProperties>
        <elementProperty name="Connections" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="Connections" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/add" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElementCollection name="add" xmlItemName="add" codeGenOptions="Indexer, AddMethod, RemoveMethod, GetItemMethods">
      <attributeProperties>
        <attributeProperty name="MaxThreads" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="MaxThreads" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <itemType>
        <configurationElementMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/ItemConfig" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="ItemConfig">
      <attributeProperties>
        <attributeProperty name="connectionName" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="connectionName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="host" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="queueName" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="queueName" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="isTransactional" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="isTransactional" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/103f9a40-1c8d-47a0-9ae5-770437a1b9ce/Boolean" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>