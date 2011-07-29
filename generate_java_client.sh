#!/bin/sh
# 
# Uses axis2 library to generate java client side code for RTWS

AXIS2_HOME=/opt/axis2-1.4.1/
WSDL_HOME=xsds

cd $WSDL_HOME
sh $AXIS2_HOME/bin/wsdl2java.sh -s -p org.letsi.rtws.client -ssi -d jaxbri -uri LETSIRTE_Service.wsdl
