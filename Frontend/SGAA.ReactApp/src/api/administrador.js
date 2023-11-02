import { handleFetch } from '/src/api/fetcher';

const getAuthorizationHeader = () => 'Bearer ' + window.localStorage.getItem('jwt');

export const getUsuarios = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/usuario', requestOptions);
}

export const getUsuario = (usuarioId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/usuario/' + usuarioId, requestOptions);
}

export const getUnidades = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/unidad', requestOptions);
}

export const getUnidad = (unidadId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/unidad/' + unidadId, requestOptions);
}

export const getAplicaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/aplicacion', requestOptions);
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
  return handleFetch('/administrador/aplicacion/' + aplicacionId, requestOptions);
}

export const getContratos = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/contrato', requestOptions);
}

export const getContrato = (contratoId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/contrato/' + contratoId, requestOptions);
}

export const getPublicaciones = () => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/publicacion', requestOptions);
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
  return handleFetch('/administrador/publicacion/' + publicacionId, requestOptions);
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
  return handleFetch('/administrador/postulacion', requestOptions);
}

export const getPostulacion = (postulacionId) => {
  const requestOptions = {
    method: 'GET',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    }
  };
  return handleFetch('/administrador/postulacion/' + postulacionId, requestOptions);
}

export const registrarUsuario = (usuario) => {
  const requestOptions = {
    method: 'POST',
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': getAuthorizationHeader(),
    },
    body: JSON.stringify(usuario)
  };
  return handleFetch('/administrador/usuario', requestOptions);
}