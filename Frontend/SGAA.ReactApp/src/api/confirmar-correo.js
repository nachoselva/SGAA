export const confirmarCorreo =
  async (email, token) => {
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: email, token: token })
    };
    return await fetch(`https://localhost:44371/usuario/confirm`, requestOptions);
  }