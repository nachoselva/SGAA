import { handleFetch } from '/src/api/fetcher';

const getAuthorizationHeader = () => 'Bearer ' + window.localStorage.getItem('jwt');

export const getAplicaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/aplicacion', requestOptions);
}

export const getAplicacion = (aplicacionId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/aplicacion/' + aplicacionId, requestOptions);
}

export const getAplicacionActive = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/aplicacion/active', requestOptions);
}

export const registrarAplicacion = (aplicacion) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(aplicacion)
  };
  return handleFetch('/inquilino/aplicacion', requestOptions);
}

export const actualizarAplicacion = (aplicacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(aplicacion)
  };
  return handleFetch('/inquilino/aplicacion/' + aplicacion.id, requestOptions);
}

export const getPostulaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/postulacion', requestOptions);
}
