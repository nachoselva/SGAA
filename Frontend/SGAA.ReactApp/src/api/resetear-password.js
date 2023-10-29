export const resetearPassword =
  async (email, token, password) => {
    const requestOptions = {
      method: 'POST',
      mode: 'cors',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: email, token: token, password: password })
    };
    return await fetch(`https://localhost:44371/usuario/reset-password`, requestOptions);
  }