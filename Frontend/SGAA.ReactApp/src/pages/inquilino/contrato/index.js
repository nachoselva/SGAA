import { Link, TableCell, TableRow } from '@mui/material';
import { useRouter } from 'next/navigation';
import { getContratos } from '/src/api/common';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { contratoStatus } from '/src/utils/status-labels';

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
        {row.domicilio}
      </TableCell>
      <TableCell>
        {row.fechaDesde}
      </TableCell>
      <TableCell>
        {row.fechaHasta}
      </TableCell>
      <TableCell>
        {contratoStatus[row.status]}
      </TableCell>
      <TableCell>
        {row.ordenRenovacion}
      </TableCell>
      <TableCell>
        {row.inquilinos}
      </TableCell>
      <TableCell>
        {row.propietarios}
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/inquilino/contrato/' + row.id + '/pago')}>
          Ver Pagos
        </Link>
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/inquilino/contrato/' + row.id)}>
          Ver Contrato
        </Link>
      </TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'domicilio', title: 'Unidad' },
      { key: 'fechaDesde', title: 'Desde' },
      { key: 'fechaHasta', title: 'Hasta' },
      { key: 'status', title: 'Estado' },
      { key: 'ordenRenovacion', title: 'N° Renovación' },
      { key: 'inquilinos', title: 'Inquilinos' },
      { key: 'propietarios', title: 'Propietarios' },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig =
    [
      { url: '/inicio', title: 'Inicio' },
      { url: '/inquilino/contrato', title: 'Contratos' }
    ];

  return (
    <FancyTablePage
      getData={getContratos}
      entityName={'Contrato'}
      listName={'Contratos'}
      breadcrumbsConfig={breadcrumbsConfig}
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
