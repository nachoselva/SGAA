import { handleFetch } from '/src/api/fetcher';

const getAuthorizationHeader = () => 'Bearer ' + window.sessionStorage.getItem('jwt');

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

export const registrarUsuario = (email, nombre, apellido, password, rol) => {
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

export const editarCurrentUsuario = (nombre, apellido) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify({ nombre: nombre, apellido: apellido })
  };
  return handleFetch('/usuario', requestOptions);
}

export const getCurrentUsuario = (nombre, apellido) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/usuario/current', requestOptions);
}