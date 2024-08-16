# endpoints

---
#### Users
-	GET `/users` - Obtener la lista de todos los usuarios
-	GET `/users/{id}` - Obtener detalles de un usuario específico
-	POST `/users` - Crear un nuevo usuario
-	PUT `/users/{id}` - Actualizar información de un usuario
-	DELETE `/users/{id}` - Eliminar un usuario
-	POST `/users/login` - Iniciar sesión para usuarios
-	POST `/users/logout` - Cerrar sesión del usuario

---
#### Shops

### ShopController Endpoints Summary

1. **`GET /api/shop/allshops`** - Retrieve all shops (Admins only).
2. **`POST /api/shop/addshop`** - Add a new shop (Shop Owners).
3. **`PUT /api/shop/updateshop/{id:guid}`** - Update shop details (Shop Owners).
4. **`GET /api/shop/getshop/{id:guid}`** - Get shop details by ID (Shop Owners).
5. **`GET /api/shop/getshop/{userId:int}`** - Get shop details by User ID (Admins only).
6. **`PUT /api/shop/{id:guid}/updatesocialmedia`** - Update shop's social media links (Shop Owners).
7. **`PUT /api/shop/{id:guid}/updateopeninghours`** - Update shop's opening hours (Shop Owners).
8. **`PUT /api/shop/{id:guid}/updategallery`** - Update shop's gallery (Shop Owners).
9. **`PUT /api/shop/{id:guid}/updatesettings`** - Update shop settings (Shop Owners).
10. **`POST /api/shop/{id:guid}/addemployee`** - Add an employee to the shop (Shop Owners).
11. **`PUT /api/shop/{id:guid}/updateemployee`** - Update employee details (Shop Owners).
12. **`DELETE /api/shop/{id:guid}/removeemployee`** - Remove an employee from the shop (Shop Owners).
13. **`POST /api/shop/{id:guid}/addreview`** - Add a review to a shop (Basic Users).
14. **`PUT /api/shop/{id:guid}/replyreview`** - Reply to a review (Basic Users).
15. **`GET /api/shop/{id:guid}/allreviews`** - Retrieve all reviews for a shop (Basic Users).

---

#### Services
-	GET `/services` - Obtener la lista de todos los servicios
-	GET `/services/{id}` - Obtener detalles de un servicio específico
-	POST `/services` - Crear un nuevo servicio
-	PUT `/services/{id}` - Actualizar información de un servicio
-	DELETE `/services/{id}` - Eliminar un servicio

---

#### Appointments
-	GET `/appointments` - Obtener la lista de todas las citas
-	GET `/appointments/{id}` - Obtener detalles de una cita específica
-	POST `/appointments` - Programar una nueva cita
-	PUT `/appointments/{id}` - Actualizar detalles de una cita
-	DELETE `/appointments/{id}` - Cancelar una cita
-	POST `/appointments/{id}/remind` - Enviar un recordatorio para una cita

---

#### Reviews
-	GET `/reviews` - Obtener la lista de todas las reseñas
-	GET `/reviews/{id}` - Obtener detalles de una reseña específica
-	POST `/reviews` - Crear una nueva reseña
-	PUT `/reviews/{id}` - Actualizar una reseña
-	DELETE `/reviews/{id}` - Eliminar una reseña
-	PUT `/reviews/{id}/visibility` - Cambiar la visibilidad de una reseña

---

#### Notifications
-	GET `/notifications` - Obtener la lista de todas las notificaciones
-	GET `/notifications/{id}` - Obtener detalles de una notificación específica
-	POST `/notifications` - Enviar una nueva notificación
-	PUT `/notifications/{id}` - Actualizar una notificación
-	DELETE `/notifications/{id}` - Eliminar una notificación

---

#### WaitList
-	GET `/waitlist` - Obtener la lista de todas las solicitudes en lista de espera
-	GET `/waitlist/{id}` - Obtener detalles de una solicitud específica
-	POST `/waitlist` - Añadir una nueva solicitud a la lista de espera
-	PUT `/waitlist/{id}` - Actualizar una solicitud en lista de espera
-	DELETE `/waitlist/{id}` - Eliminar una solicitud de la lista de espera

---
#### ShopSettings
-	GET `/shops/{id}/settings` - Obtener la lista de configuraciones de un local
-	PUT `/shops/{id}/settings` - Actualizar configuraciones de un local

