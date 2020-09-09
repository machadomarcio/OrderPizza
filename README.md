# OrderPizza
Projeto desenvolvido em .Net Core para aprendizado

Utilizando .Net Core 3.1 EntityFramework 3.1

Utilizado Documentação no swagger.

criado uma API para receber os pedidos feitos a partir do site da pizzaria que atenda aos requisitos abaixo:

• Todo pedido precisa ter um identificador único

• Um pedido pode ter no mínimo uma pizza e no máximo 10.
• Cada pizza pode ter até dois sabores, os sabores disponíveis são:
3 Queijos (R$ 50), Frango com requeijão (R$ 59.99), Mussarela (R$ 42.50), Calabresa (R$ 42.50), Pepperoni (R$ 55), Portuguesa (R$ 45), Veggie (R$ 59.99)
• O preço da pizza com dois sabores (meio a meio) deve ser composto pela metade do valor de cada uma das pizzas.
• O cliente não precisa ter cadastro para fazer um pedido, mas nesse caso precisará informar os dados de endereço de entrega, caso seja um cliente cadastrado ele não precisa informar o endereço de entrega, pois deve constar em seu cadastro.
• Não vamos cobrar frete nessa primeira versão do sistema
• O cliente deve ser capaz de ver seu histórico de pedidos, com os detalhes das pizzas, valor individual e valor total do pedido.


Implementações Futuras:
- Inserção de Logs;
- Configuração valor de entrega;
- Autenticação;
