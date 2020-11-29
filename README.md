# NetCoreSagaSample


1. Configure a imagem do Redis ================================================================================================================

	docker pull redis
	docker run -d --name saga_orders_state  -e REDIS_PASSWORD=adm!adm -p 6379:6379 --restart=always redis
	docker exec -it saga_orders_state redis-cli
	ping 
	# Resposta: pong


2. Configure a imagem do Rabbit ===============================================================================================================

	docker pull rabbitmq:3-management
	docker run -d --name saga_order_bus -p 5672:5672 -p 15672:15672 --restart=always --hostname localhost -v /docker/rabbitmq/data:/var/lib/rabbitmq -e RABBITMQ_DEFAULT_USER=saga -e RABBITMQ_DEFAULT_PASS=saga -e RABBITMQ_DEFAULT_VHOST=saga_order_state_dev rabbitmq:3-management
	docker stop rabbitmq
	docker start rabbitmq
	docker logs -f rabbitmq

	# Acesso à interface: http://localhost:15672/