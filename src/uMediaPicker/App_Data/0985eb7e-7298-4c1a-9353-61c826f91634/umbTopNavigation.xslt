<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE xsl:stylesheet [
    <!ENTITY nbsp "&#x00A0;">
]>
<xsl:stylesheet
  version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxml="urn:schemas-microsoft-com:xslt"
  xmlns:umbraco.library="urn:umbraco.library"
  exclude-result-prefixes="msxml umbraco.library">


    <xsl:output method="xml" omit-xml-declaration="yes" />

    <xsl:param name="currentPage"/>

    <!-- Input the documenttype you want here -->
    <xsl:variable name="level" select="1"/>

    <xsl:template match="/">

 <ul>
   <li class="home navigation">
      <xsl:attribute name="class">
        <xsl:choose>
          <xsl:when test="$currentPage/@level=1">
            home active
          </xsl:when>
          <xsl:otherwise>
            home navigation
          </xsl:otherwise>
        </xsl:choose>

     </xsl:attribute> 
     <a href="/">Home</a>
       
   </li>
      <xsl:for-each select="$currentPage/ancestor-or-self::* [@level=$level]/* [@isDoc and string(umbracoNaviHide) != '1']">
  <li>
        <xsl:attribute name="class"><xsl:call-template name="isActive">
          <xsl:with-param name="currentPage" select="."/>
          </xsl:call-template>
       </xsl:attribute>    
    
    <a  href="{umbraco.library:NiceUrl(@id)}">
      <span><xsl:value-of select="@nodeName"/></span>
    </a>
  </li>
  
  
</xsl:for-each>
</ul>

    </xsl:template>
<xsl:template name="isActive">
  <xsl:param name="currentpage"/>
  <xsl:choose>
<xsl:when test="$currentPage/ancestor-or-self::*/@id=@id">
  active
</xsl:when>
<xsl:otherwise>
  navigation
</xsl:otherwise>
</xsl:choose>

</xsl:template>
</xsl:stylesheet>