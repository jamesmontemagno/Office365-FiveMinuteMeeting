Réunion de cinq minutes : Office 365 et Xamarin Sample
=========================

Office 365 Navigateur de contact, éditeur, et possibilité de réunions à votre calendrier et d'organiser vos contacts également.

Pour plus d’informations, consultez ma session Office 365 sur Ignite 2015 : http://channel9.msdn.com/Events/Ignite/2015/BRK3342

## AVERTISSEMENT
Cet exemple utilise une version d’évaluation d’ADAL et il a été observé qu’il y aura des modifications à l’avenir. Alors, n’hésitez pas à le faire. Pour plus d’informations, consultez ce blog : http://www.cloudidentity.com/blog/2014/10/30/adal-net-v3-preview-pcl-xamarin-support/

<h3>Mise à jour le 26 mai 2015</h3>
Avec l’actualisation de mars de la version d’évaluation ADAL.v3 plusieurs améliorations ont été apportées en relation avec Xamarin 
<ul>
	<li>
		ADAL passe à la nouvelle API unifiée Xamarin pour iOS
	</li>
	<li>
		Fournit une bibliothèque de classe portable avec une authentification facile à utiliser
	</li>
</ul>
<p>

Comprendre qu’il s’agit d’une version d’évaluation et ne doit pas être utilisé en production, ces améliorations facilitent l’utilisation d’ADAL dans nos projets. Veuillez référencer ce billet de blog [ici] (http://www.cloudidentity.com/blog/2015/03/04/adal-v3-preview-march-refresh/) pour en savoir plus sur l’actualisation de mars et sur ce blog [ici] (https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/3.1.203031538-alpha) pour voir tous les éléments inclus dans le package NuGet pour la dernière build ADAL.

De plus, pour d’autres API Office 365, consultez leur GitHub : http://github.com/officedev.

Nous utilisons également une version d’évaluation de ADAL, vous devez configurer les packages MyGet :

* accédez à l’Explorateur de solutions-> [nœud de projet]-> gérer les packages NuGet...-> Paramètres.
* Appuyez sur le bouton + dans le coin supérieur droit
* dans le champ Name (nom), entrez quelque chose pour l’effet « AAD Nightly »
* dans le champ source, entrez http://www.myget.org/f/azureadwebstacknightly/
* Appuyez sur « Mettre à jour »
* Appuyez sur OK


### obtenir de l’aide

si vous avez des questions, trouvez simplement un membre du personnel Xamarin à évoluer 2014.

Cette application mobile vous est apportée par vos amis sur [Xamarin] (http://www.xamarin.com/).

### Captures d’écran 

! [Capture d’écran Android d’application montrant la liste des contacte] (Screenshots/android1.png) ! [Capture d’écran Android de la page détails du contact] (Screenshots/android2.png) 
! [Capture d’écran iOS d’une application avec une liste de contact] (Screenshots/ios1.png) ! [Capture d’écran iOS de la page détails du contact](Screenshots/ios2.png)

### Développement par :
-James Montemagno : [Twitter] (http://www.twitter.com/jamesmontemagno) | [Blog](http://motzcod.es) | [GitHub](http://www.github.com/jamesmontemagno)
