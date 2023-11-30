import ArchiveBoxIcon from '@heroicons/react/24/solid/ArchiveBoxIcon';
import BriefcaseIcon from '@heroicons/react/24/solid/BriefcaseIcon';
import CurrencyDollarIcon from '@heroicons/react/24/solid/CurrencyDollarIcon';
import DocumentTextIcon from '@heroicons/react/24/solid/DocumentTextIcon';
import HomeIcon from '@heroicons/react/24/solid/HomeIcon';
import HomeModernIcon from '@heroicons/react/24/solid/HomeModernIcon';
import InboxStackIcon from '@heroicons/react/24/solid/InboxStackIcon';
import NewsPaperIcon from '@heroicons/react/24/solid/NewsPaperIcon';
import QuestionMarkCircleIcon from '@heroicons/react/24/solid/QuestionMarkCircleIcon';
import ShieldCheckIcon from '@heroicons/react/24/solid/ShieldCheckIcon';
import UserGroupIcon from '@heroicons/react/24/solid/UserGroupIcon';
import { SvgIcon } from '@mui/material';

export const getMenuItems = (authContext) => {
  const { isAuthenticated, user } = authContext;
  const everyone = [];
  const administrador = [];
  const propietario = [];
  const inquilino = [];
  const terms = [];

  everyone.push(
    {
      title: 'Inicio',
      path: '/inicio',
      icon: (
        <SvgIcon fontSize="small">
          <HomeIcon />
        </SvgIcon>
      )
    });

  if (isAuthenticated) {
    if (user.licencia == 'ProyectoFinal') {
      everyone.push({
        title: 'Publicaciones',
        path: '/publicacion',
        icon: (
          <SvgIcon fontSize="small">
            <NewsPaperIcon />
          </SvgIcon>
        )
      });
    }

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

      if (user.licencia == 'ProyectoFinal') {
        administrador.push(
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
            title: 'Pagos',
            path: '/administrador/pago',
            icon: (
              <SvgIcon fontSize="small">
                <CurrencyDollarIcon />
              </SvgIcon>
            )
          });
      }
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
        },
        {
          title: 'Mis Publicaciones',
          path: '/propietario/publicacion',
          icon: (
            <SvgIcon fontSize="small">
              <NewsPaperIcon />
            </SvgIcon>
          )
        });

      if (user.licencia == 'ProyectoFinal') {
        propietario.push(
          {
            title: 'Mis Contratos',
            path: '/propietario/contrato',
            icon: (
              <SvgIcon fontSize="small">
                <DocumentTextIcon />
              </SvgIcon>
            )
          }
        );
      }
    }
    if (user.roles.includes('Inquilino')) {
      inquilino.push(
        {
          title: 'Mis Aplicaciones',
          path: '/inquilino/aplicacion',
          icon: (
            <SvgIcon fontSize="small">
              <HomeModernIcon />
            </SvgIcon>
          )
        },
        {
          title: 'Mis Postulaciones',
          path: '/inquilino/postulacion',
          icon: (
            <SvgIcon fontSize="small">
              <BriefcaseIcon />
            </SvgIcon>
          )
        });

      if (user.licencia == 'ProyectoFinal') {
        inquilino.push(
          {
            title: 'Mis Contratos',
            path: '/inquilino/contrato',
            icon: (
              <SvgIcon fontSize="small">
                <DocumentTextIcon />
              </SvgIcon>
            )
          }
        );
      }
    }
    terms.push(
      {
        title: 'Términos y Condiciones',
        path: '/tyc',
        icon: (
          <SvgIcon fontSize="small">
            <ArchiveBoxIcon />
          </SvgIcon>
        )
      },
      {
        title: 'Política de Privacidad',
        path: '/privacidad',
        icon: (
          <SvgIcon fontSize="small">
            <ShieldCheckIcon />
          </SvgIcon>
        )
      },
      {
        title: 'Ayuda',
        path: '/ayuda',
        icon: (
          <SvgIcon fontSize="small">
            <QuestionMarkCircleIcon />
          </SvgIcon>
        )
      });
  }

  return { everyone, administrador, propietario, inquilino, terms };
}

