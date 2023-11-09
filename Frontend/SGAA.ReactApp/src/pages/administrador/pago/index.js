import { Link, TableCell, TableRow } from '@mui/material';
import Moment from 'moment';
import { useRouter } from 'next/navigation';
import { getPagos } from '/src/api/administrador';
import { FancyDownloadButton } from '/src/components/fancy-download-button';
import { FancyTablePage } from '/src/components/fancy-table-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { pagoStatus } from '/src/utils/status-labels';

const Page = (props) => {

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
        {row.descripcion}
      </TableCell>
      <TableCell>
        {row.monto}
      </TableCell>
      <TableCell>
        {row.fechaVencimiento && Moment(row.fechaVencimiento).format('DD/MM/yyyy')}
      </TableCell>
      <TableCell>
        {pagoStatus[row.status]}
      </TableCell>
      <TableCell>
        {row.fechaPago && Moment(row.fechaPago).format('DD/MM/yyyy')}
      </TableCell>
      <TableCell>
        {row.domicilio}
      </TableCell>
      <TableCell>
        {row.archivo &&
          <FancyDownloadButton currentFile={JSON.parse(row.archivo)} />
        }
      </TableCell>
      <TableCell>
        <Link
          component="button"
          underline="hover"
          color="inherit"
          onClick={() => router.push('/administrador/pago/' + row.id)}>
          Ver Pago
        </Link>
      </TableCell>
      <TableCell>
        {
          <Link
            component="button"
            underline="hover"
            color="inherit"
            onClick={() => router.push('/administrador/contrato/' + row.contratoId)}>
            Ver Contrato
          </Link>
        }</TableCell>
    </TableRow>);

  const headerConfiguration =
    [
      { key: 'id', title: '#' },
      { key: 'descripcion', title: 'Descripción' },
      { key: 'monto', title: 'Monto' },
      { key: 'fechaVencimiento', title: 'Fecha de Vencimiento' },
      { key: 'pagoStatus', title: 'Estado' },
      { key: 'fechaPago', title: 'Fecha de Pago' },
      { key: 'domicilio', title: 'Domicilio' },
      { key: 'archivo', title: 'Comprobante de Pago' },
      { key: null, title: null },
      { key: null, title: null }
    ];

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/administrador/pago', title: 'Pagos' }
  ];

  return (
    <FancyTablePage
      getData={getPagos}
      entityName={'Pago'}
      listName={'Pagos'}
      breadcrumbsConfig={breadcrumbsConfig}
      roles={['Administrador']}
      headerConfiguration={headerConfiguration}
      tableRowGenerator={tableRowGenerator}
      onAddEntityRedirectTo={'/administrador/pago/crear'}
    />
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
