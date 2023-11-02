import HomeModernIcon from '@heroicons/react/24/solid/HomeModernIcon';
import HomeIcon from '@heroicons/react/24/solid/HomeIcon';
import InboxStackIcon from '@heroicons/react/24/solid/InboxStackIcon';
import NewsPaperIcon from '@heroicons/react/24/solid/NewsPaperIcon';
import UserGroupIcon from '@heroicons/react/24/solid/UserGroupIcon';
import DocumentTextIcon from '@heroicons/react/24/solid/DocumentTextIcon';
import BriefcaseIcon from '@heroicons/react/24/solid/BriefcaseIcon';
import { SvgIcon } from '@mui/material';
import { useAuthContext } from '/src/contexts/auth-context';

export const getMenuItems = () => {
  const { isAuthenticated, user } = useAuthContext();
  let everyone = [];
  let administrador = [];
  let propietario = [];
  let inquilino = [];
  if (isAuthenticated) {
    everyone.push(
      {
        title: 'Inicio',
        path: '/',
        icon: (
          <SvgIcon fontSize="small">
            <HomeIcon />
          </SvgIcon>
        )
      });
    if (user.roles.includes('Administrador')) {
      administrador.push(
        {
          title: 'Unidades',
          path: '/administrador/unidad',
          icon: (
            <SvgIcon fontSize="small">
              <HomeModernIcon />
            </SvgIcon>
          )
        },
        {
          title: 'Contratos',
          path: '/administrador/contrato',
          icon: (
            <SvgIcon fontSize="small">
              <DocumentTextIcon />
            </SvgIcon>
          )
        },
        {
          title: 'Publicaciones',
          path: '/administrador/publicacion',
          icon: (
            <SvgIcon fontSize="small">
              <NewsPaperIcon />
            </SvgIcon>
          )
        },
        {
          title: 'Aplicaciones',
          path: '/administrador/aplicacion',
          icon: (
            <SvgIcon fontSize="small">
              <InboxStackIcon />
            </SvgIcon>
          )
        },
        {
          title: 'Postulaciones',
          path: '/administrador/postulacion',
          icon: (
            <SvgIcon fontSize="small">
              <BriefcaseIcon />
            </SvgIcon>
          )
        },
        {
          title: 'Usuarios',
          path: '/administrador/usuario',
          icon: (
            <SvgIcon fontSize="small">
              <UserGroupIcon />
            </SvgIcon>
          )
        });
    }
    if (user.roles.includes('Propietario')) {
      propietario.push(
        {
          title: 'Mis Unidades',
          path: '/propietario/unidad',
          icon: (
            <SvgIcon fontSize="small">
              <HomeModernIcon />
            </SvgIcon>
          )
        });
    }
  }
  return { everyone, administrador, propietario, inquilino };
}

