$('document').ready(function(){

	// //////////////// Validação do formulário de Cadastro ////////////////
	$("#form-cad").validate({
		rules: {
			'nome-cad' : 'required',
			'razao-cad' : 'required',
			'titular-cad' : 'required',
			'login-cad' : {
				required: true,
				minlength: 5
			},
			'senha-cad' : {
				required: true,
				minlength: 3
			},
			'senha2-cad' : {
				required: true,
				equalTo: '#senha-cad'
			},
			'email-cad' : {
				required: true,
				email: true
			},
			'cep-cad' : {
				required: true
			}
		},
		messages: {
			'nome-cad': 'Por favor, digite seu nome',
			'razao-cad': 'Por favor, digite a razão social',
			'titular-cad': 'Por favor, coloque o titular',
			'login-cad': {
				required: 'Por favor, digite o login',
				minlength: 'O login deve ter mais do que 5 caracteres'
			},
			'senha-cad': {
				required: 'Por favor, digite a senha',
				minlength: 'A senha deve ter mais do que 5 caracteres'
			},
			'senha2-cad': {
				required: 'Por favor, digite a senha novamente',
				equalTo: 'Senha difere da acima'
			},
			'email-cad': {
				required: 'Por favor, digite o email',
				email: 'Digite um email válido'
			},
			'cep-cad': 'Por favor, digite o CEP'
		}
	});

	// função para autocompletar endereço utilizando CEP
	$('#cep-cad').cep();

	// //////////////// Validação do formulário de Simulação ////////////////
	$('#form-prod').validate({
		rules:{
			'classe-prod': {
				min: 1
			},
			'subclasse-prod': {
				min: 1
			},
			'item-prod': {
				min: 1
			},
			'armazenagem-prod': {
				min: 1
			},
			'qtde-prod': 'required',
			'dt-inicio-prod': 'required',
			'dt-fim-prod': 'required'
		},
		messages: {
			'classe-prod': {
				min: 'Por favor, escolha uma classe para prosseguir'
			},
			'subclasse-prod': {
				min: 'Por favor, escolha uma subclasse para prosseguir'
			},
			'item-prod': {
				min: 'Por favor, escolha um produto para prosseguir'
			},
			'armazenagem-prod': {
				min: 'Por favor, escolha um tipo de aramzenamento'
			},
			'qtde-prod': 'Por favor, digite uma quantidade',
			'dt-inicio-prod': 'Por favor, coloque a data de início do armazenamento',
			'dt-fim-prod': 'Por favor, coloque a data de fim do armazenamento'
		}
	});

	// //////////////// Validação do formulário de Simulação ////////////////

	$('#form-cont').validate({
		rules: {
			'nome-cont': 'required',
			'email-cont': {
				required: true,
				email: true
			},
			'texto-cont': 'required'
		},
		messages: {
			'nome-cont': 'Por favor, digite o seu nome',
			'email-cont': {
				required: 'Por favor, digite o seu e-mail',
				email: 'Digite um e-mail válido'
			},
			'texto-cont': 'Vai mandar uma mensagem vazia? Escreva-nos algo'
		}
	});

	$('#form-login').validate({
		rules: {
			login: {
				required: true
			},
			senha: {
				required: true
			}
		},
		messages: {
			login: {
				required: 'Por favor, digite um login'
			}, 
			senha: {
				required: 'Por favor, digite uma senha'
			}
		}
	});

});