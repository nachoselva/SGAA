import { handleFetch } from '/src/api/fetcher';

export const login = (email, password) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email: email, password: password })
  };
  return handleFetch('/security/login', requestOptions);
}

export const confirmarCorreo = (email, token) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email: email, token: token })
  };
  return handleFetch('/usuario/confirm', requestOptions);
}

export const recuperarPassword = (email) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email: email })
  };
  return handleFetch('/usuario/forgot-password', requestOptions);
}

export const registrar = (email, nombre, apellido, password, rol) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email: email, nombre: nombre, apellido: apellido, rol: rol, password: password })
  };
  return handleFetch('/usuario', requestOptions);
}

export const resetearPassword = (email, token, password) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email: email, token: token, password: password })
  };
  return handleFetch('/usuario/reset-password', requestOptions);
}