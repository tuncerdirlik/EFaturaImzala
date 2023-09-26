using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EFaturaImzala
{
    internal class XmlHelper
    {
        public static XmlNamespaceManager GetXmlNsManager(XmlDocument doc)
        {
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            namespaceManager.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaceManager.AddNamespace("xades", "http://uri.etsi.org/01903/v1.3.2#");
            namespaceManager.AddNamespace("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            namespaceManager.AddNamespace("ccts", "urn:un:unece:uncefact:documentation:2");
            namespaceManager.AddNamespace("ubltr", "urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents");
            namespaceManager.AddNamespace("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            namespaceManager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            namespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            namespaceManager.AddNamespace("ef", "http://www.efatura.gov.tr/package-namespace");
            namespaceManager.AddNamespace("sh", "http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader");
            namespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            namespaceManager.AddNamespace("sch", "http://purl.oclc.org/dsdl/schematron");
            namespaceManager.AddNamespace("urn", "urn:oasis:names:specification:ubl:schema:xsd:ApplicationResponse-2");
            namespaceManager.AddNamespace("urn1", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            namespaceManager.AddNamespace("urn2", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            return namespaceManager;
        }
    }
}
