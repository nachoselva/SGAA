import { handleFetch } from '/src/api/fetcher';

export const getUsuarios = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + window.sessionStorage.getItem('jwt'),
    }
  };
  return handleFetch('/administrador/usuario', requestOptions);
}

export const getUnidades = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + window.sessionStorage.getItem('jwt'),
    }
  };
  return handleFetch('/administrador/unidad', requestOptions);
}
