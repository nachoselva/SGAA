import { Link, TableCell, TableRow } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getPostulaciones } from '/src/api/inquilino';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';

const Page = () => {

  const router = useRouter();

  const tableRowGenerator = (row) => (
    <TableRow
      hover
      key={row.id}
    >
      <TableCell>
        {row.id}
      </TableCell>
      <TableCell>
        {row.domicilioCompleto}
      </TableCell>
      <TableCell>
        {row.montoAlquiler}
      </TableCell>
      <TableCell>
        {row.status}
      </TableCell>
      <TableCell>
        {row.fechaPostulacion}
      </TableCell>
      <TableCell>
        {row.fechaOferta}
      </TableCell>
      <TableCell>
        {
          row.status == 'Ofrecida'
          &&
          <Link
              component="button"
              underline="hover"
              color="inherit"
              onClick={() => { } }>
            Aceptar
          </Link>
        }
      </TableCell>
      <TableCell>
        {
          row.status == 'Ofrecida'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => { }}>
            Rechazar
          </Link>
        }
      </TableCell>
      <TableCell>
        {
          row.status == 'Postulada'
          &&
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => { }}>
            Cancelar
          </Link>
        }
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/postulacion/' + row.id)}>
          Ver Postulación
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/aplicacion/' + row.aplicacionId)}>
          Ver Aplicación
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/publicacion/' + row.publicacionId)}>
          Ver Publicación
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'domicilioCompleto', title: 'Unidad' },
      { key: 'montoAlquiler', title: 'Monto Alquiler' },
      { key: 'status', title: 'Estado' },
      { key: 'fechaPostulacion', title: 'Fecha Postulación' },
      { key: 'fechaOferta', title: 'Fecha Oferta' },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/inquilino/postulacion', title: 'Postulaciones' }
  ];

  return (
    <FancyTablePage
      getData={getPostulaciones}
      entityName={'Postulacion'}
      listName={'Postulaciones'}
      breadcrumbsConfig={breadcrumbsConfig}
      roles={['Inquilino']}
      headerConfiguration={headerConfiguration}
      tableRowGenerator={tableRowGenerator}
    />
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
