Reunião de Cinco Minutos - Exemplo do Office 365 e do Xamarin 
=========================

O Office 365 Navegador de contatos, editor e reuniões de capacidade para o seu calendário e também organiza seus contatos.

Para saber mais, confira minha sessão sobre o Office 365 no Ignite 2015: http://channel9.msdn.com/Events/Ignite/2015/BRK3342

## ADVERTÊNCIA
Esta amostra está usando uma edição de visualização do ADAL e foi observado que haverá alterações significativas no futuro. Favor estar atento a isso. Confira este blog para saber mais: http://www.cloudidentity.com/blog/2014/10/30/adal-net-v3-preview-pcl-xamarin-support/

<h3>Atualização em 26 de maio de 2015</h3>
Com a atualização de março da visualização ADAL.v3, várias melhorias foram implementadas no que diz respeito ao Xamarin 
<ul>
	<li>
		O ADAL mudou-se para a nova API unificada do Xamarin para iOS
	</li>
	<li>
		Fornece uma Biblioteca de Classe Portátil com autenticação fácil de usar
	</li>
</ul>
<p>

Entendendo que isso ainda está na fase de Visualização e não deve ser usado na Produção, essas melhorias facilitam o trabalho com o ADAL em nossos projetos. Consulte esta postagem de blog [here] (http://www.cloudidentity.com/blog/2015/03/04/Adal-v3-Preview-March-Refresh/) para saber mais sobre a atualização de março e este blog [here] (https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/3.1.203031538-alpha) para ver tudo o que acompanha o pacote NuGet para o build do ADAL mais recente.

Além disso, para obter mais APIs do Office 365, consulte o GitHub: http://github.com/officedev

Também estamos usando uma versão de visualização do ADAL, você deve configurar pacotes de MyGet:

* Vá para o Gerenciador de soluções-> [project node]-> Gerenciar pacotes NuGet...-> Configurações.
* Pressione o botão ' + ' no canto superior direito
* No campo nome, digite algo para o efeito de "AAD noturno"
*, no campo de origem, digite http://www.myget.org/f/azureadwebstacknightly/
* Clique em "Atualizar"
* Pressione OK


### Obter Ajuda

Se você tiver dúvidas, basta encontrar um membro da equipe do Xamarin no Evolve 2014.

O aplicativo móvel foi criado por seus amigos no [Xamarin] (http://www.xamarin.com/).

### Capturas de tela!

![Captura de tela do aplicativo para Android mostrando a lista de pessoas] (Capturas de tela/android1. png)! [Captura de tela da página de detalhes da pessoa] (Capturas de tela/ANDROID2. png)
![captura de tela de aplicativo para IOS mostrando lista de pessoas] (Capturas de tela/IOS1. png)! [captura de tela em IOS da página de detalhes da pessoa] (Capturas de tela/ios2. png)

### Desenvolvido por:
-James Montemagno: [Twitter] (http://www.twitter.com/jamesmontemagno) | Bloga (http://motzcod.es) | GitHub (http://www.github.com/jamesmontemagno)
