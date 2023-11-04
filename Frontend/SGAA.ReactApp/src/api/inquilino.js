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

export const getPostulacion = (id) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/postulacion/' + id, requestOptions);
}

export const cancelarPostulacion = (postulacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: {}
  };
  return handleFetch('/inquilino/postulacion/' + postulacion.id +'/cancelar', requestOptions);
}

export const aceptarOferta = (postulacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: {}
  };
  return handleFetch('/inquilino/postulacion/' + postulacion.id + '/oferta/aceptar', requestOptions);
}

export const rechazarOferta = (postulacion) => {
  const requestOptions = {
    method: 'PUT',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: {}
  };
  return handleFetch('/inquilino/postulacion/' + postulacion.id + '/oferta/rechazar', requestOptions);
}

export const getPublicacion = (publicacionId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/inquilino/publicacion/' + publicacionId, requestOptions);
}