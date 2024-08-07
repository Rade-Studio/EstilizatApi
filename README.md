### endpoints

#### Users
   -	GET `/users` - Obtener la lista de todos los usuarios
   -	GET `/users/{id}` - Obtener detalles de un usuario específico
   -	POST `/users` - Crear un nuevo usuario
   -	PUT `/users/{id}` - Actualizar información de un usuario
   -	DELETE `/users/{id}` - Eliminar un usuario
   -	POST `/users/login` - Iniciar sesión para usuarios
   -	POST `/users/logout` - Cerrar sesión del usuario
#### Shops
   -	GET `/shops` - Obtener la lista de todos los locales
   -	GET `/shops/{id}` - Obtener detalles de un local específico
   -	POST `/shops` - Registrar un nuevo local
   -	PUT `/shops/{id}` - Actualizar información de un local
   -	DELETE `/shops/{id}` - Eliminar un local
   -	GET `/shops/{id}/services` - Obtener la lista de servicios de un local
   -	POST `/shops/{id}/services` - Añadir un nuevo servicio a un local
#### Services
   -	GET `/services` - Obtener la lista de todos los servicios
   -	GET `/services/{id}` - Obtener detalles de un servicio específico
   -	POST `/services` - Crear un nuevo servicio
   -	PUT `/services/{id}` - Actualizar información de un servicio
   -	DELETE `/services/{id}` - Eliminar un servicio
#### Appointments
   -	GET `/appointments` - Obtener la lista de todas las citas
   -	GET `/appointments/{id}` - Obtener detalles de una cita específica
   -	POST `/appointments` - Programar una nueva cita
   -	PUT `/appointments/{id}` - Actualizar detalles de una cita
   -	DELETE `/appointments/{id}` - Cancelar una cita
   -	POST `/appointments/{id}/remind` - Enviar un recordatorio para una cita
#### Reviews
   -	GET `/reviews` - Obtener la lista de todas las reseñas
   -	GET `/reviews/{id}` - Obtener detalles de una reseña específica
   -	POST `/reviews` - Crear una nueva reseña
   -	PUT `/reviews/{id}` - Actualizar una reseña
   -	DELETE `/reviews/{id}` - Eliminar una reseña
   -	PUT `/reviews/{id}/visibility` - Cambiar la visibilidad de una reseña
#### Notifications
   -	GET `/notifications` - Obtener la lista de todas las notificaciones
   -	GET `/notifications/{id}` - Obtener detalles de una notificación específica
   -	POST `/notifications` - Enviar una nueva notificación
   -	PUT `/notifications/{id}` - Actualizar una notificación
   -	DELETE `/notifications/{id}` - Eliminar una notificación
#### WaitList
   -	GET `/waitlist` - Obtener la lista de todas las solicitudes en lista de espera
   -	GET `/waitlist/{id}` - Obtener detalles de una solicitud específica
   -	POST `/waitlist` - Añadir una nueva solicitud a la lista de espera
   -	PUT `/waitlist/{id}` - Actualizar una solicitud en lista de espera
   -	DELETE `/waitlist/{id}` - Eliminar una solicitud de la lista de espera
#### ShopSettings
   -	GET `/shops/{id}/settings` - Obtener la lista de configuraciones de un local
   -	PUT `/shops/{id}/settings` - Actualizar configuraciones de un local



