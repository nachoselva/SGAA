export const recuperarPassword =
  async (email) => {
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: email })
    };
    return await fetch(`https://localhost:44371/usuario/forgot-password`, requestOptions);
  }