import { Link, TableCell, TableRow } from '@mui/material';
import { useRouter } from 'next/navigation';
import { useEffect, useRef, useState } from 'react';
import { getAplicacionActive, getAplicaciones } from '/src/api/inquilino';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const Page = () => {

  const router = useRouter();
  const [activeAplicacion, setActiveAplicacion] = useState(null);
  const initialized = useRef(false);

  useEffect(() => {
    if (!initialized.current) {
      initialized.current = true;

      getAplicacionActive()
        .then((response) => {
          setActiveAplicacion(response);
        })
        .catch((err) => {
          if (err.statusCode != 404) {
            throw err;
          }
        });
    }
  }, []);

  const tableRowGenerator = (row) => (
    <TableRow
      hover
      key={row.id}
    >
      <TableCell>
        {row.id}
      </TableCell>
      <TableCell>
        {row.status}
      </TableCell>
      <TableCell>
        {row.puntuacionTotal}
      </TableCell>
      <TableCell>
        {row.postulaciones}
      </TableCell>
      <TableCell>
        {
          row.status == 'AprobacionPendiente'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.push('/inquilino/aplicacion/' + row.id + '/editar')}>
            Editar Aplicación
          </Link>
        }
        {
          row.status != 'AprobacionPendiente'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.push('/inquilino/aplicacion/' + row.id)}>
            Ver Aplicación
          </Link>
        }
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'status', title: 'Estado' },
      { key: 'puntuacionTotal', title: 'Puntuación' },
      { key: 'postulaciones', title: 'Postulaciones' },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/inquilino/aplicacion', title: 'Aplicaciones' }
  ];

  return (
    <FancyTablePage
      getData={getAplicaciones}
      entityName={'Aplicacion'}
      listName={'Aplicaciones'}
      breadcrumbsConfig={breadcrumbsConfig}
      roles={['Inquilino']}
      headerConfiguration={headerConfiguration}
      tableRowGenerator={tableRowGenerator}
      onAddEntityRedirectTo={!activeAplicacion ? '/inquilino/aplicacion/crear' : null}
    />
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
