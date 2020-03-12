Five Minute Meeting: ejemplo de Office 365 y Xamarin
=========================

Office 365: explorar y editar contactos, crear reuniones en el calendario y organizar los contactos.

Para obtener más información, consulte mi sesión de Office 365 en Ignite 2015: http://channel9.msdn.com/Events/Ignite/2015/BRK3342

## ADVERTENCIA
En este ejemplo se usa una edición preliminar de ADAL y se ha indicado que habrá cambios importantes en el futuro. Téngalo en cuenta. Puede ver más información en el blog: http://www.cloudidentity.com/blog/2014/10/30/adal-net-v3-preview-pcl-xamarin-support/

<h3>Actualizado el 26 de mayo de 2015</h3>
Con la actualización marzo de la versión preliminar de ADAL.v3, se han realizado algunas mejoras en lo que se refiere a Xamarin 
<ul>
	<li>
		ADAL se mueve a la nueva API unificada de Xamarin para iOS
	</li>
	<li>
		Ofrece una biblioteca de clases portable con autenticación fácil de usar.
	</li>
</ul>
<p>

Teniendo en cuenta que esta es todavía una versión preliminar y que no debe usarse en producción, estas mejoras facilitan el trabajo con ADAL en los proyectos.Consulte esta entrada de blog [aquí](http://www.cloudidentity.com/blog/2015/03/04/adal-v3-preview-march-refresh/) para obtener más información sobre la actualización de marzo, y este blog [aquí](https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/3.1.203031538-alpha) para ver todo lo que se incluye con el paquete NuGet para la última compilación de ADAL.

Además, encontrará otras API de Office 365 en su GitHub: http://github.com/officedev

Estamos usando también una versión preliminar de ADAL, debe configurar los paquetes de MyGet:

* Vaya al Explorador de soluciones->[nodo de proyecto]-> Administrar paquetes de NuGet…->Configuración.
* Presione el botón "+" de la esquina superior derecha
* En el campo Nombre, introduzca algo como “AAD por la noche”
* En el campo Origen, introduzca http://www.myget.org/f/azureadwebstacknightly/
* Presione "Actualizar"
* Presione "Aceptar"


### Obtenga ayuda

si tiene alguna pregunta, simplemente busque algún miembro del personal de Xamarin en Evolve 2014.

Aplicación móvil presentada por el equipo de [Xamarin](http://www.xamarin.com/).

### Capturas de pantalla

![Captura de pantalla de Android de una lista de personas que se muestra en la aplicación](Screenshots/android1.png)![Captura de pantalla de Android de la página de detalles de una persona](Screenshots/android2.png)
![Captura de pantalla de iOS de una lista de personas que se muestra en la aplicación](Screenshots/ios1.png)![Captura de pantalla de iOS de la página de detalles de una persona](Screenshots/ios2.png)

### Desarrollo de:
- James Montemagno: [Twitter](http://www.twitter.com/jamesmontemagno) | [Blog](http://motzcod.es) | [GitHub](http://www.github.com/jamesmontemagno)
