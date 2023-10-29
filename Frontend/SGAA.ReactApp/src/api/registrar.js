export const registrar =
  async (email, nombre, apellido, password, rol) => {
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: email, nombre: nombre, apellido: apellido, rol: rol, password: password })
    };
    return await fetch(`https://localhost:44371/usuario`, requestOptions);
  }