import ArrowDownOnSquareIcon from '@heroicons/react/24/solid/ArrowDownOnSquareIcon';
import ArrowUpOnSquareIcon from '@heroicons/react/24/solid/ArrowUpOnSquareIcon';
import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { Box, Button, Container, Stack, SvgIcon, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useMemo, useState } from 'react';
import { AuthGuard } from '/src/guards/auth-guard';
import { useSelection } from '/src/hooks/use-selection';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PostulacionesSearch } from '/src/sections/postulacion/postulacion-search';
import { PostulacionesTable } from '/src/sections/postulacion/postulacion-table';
import { applyPagination } from '/src/utils/apply-pagination';

const data = [
  {
    id: 1,
    publicacionId: 1,
    aplicacionId: 1,
    status: 'Postulada',
    postulantes: 'lista de postulantes',
    fechaPostulacion: '2023/10/10',
    fechaOferta: '2023/10/10',
    montoAlquiler: 2000,
    domicilioCompleto: 'Corrientes 1500 2A'
  }
];

const usePostulaciones = (page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(data, page, rowsPerPage);
    },
    [page, rowsPerPage]
  );
};

const usePostulacionIds = (postulaciones) => {
  return useMemo(
    () => {
      return postulaciones.map((postulacion) => postulacion.id);
    },
    [postulaciones]
  );
};

const Page = () => {
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const postulaciones = usePostulaciones(page, rowsPerPage);
  const postulacionesIds = usePostulacionIds(postulaciones);
  const postulacionesSelection = useSelection(postulacionesIds);

  const handlePageChange = useCallback(
    (event, value) => {
      setPage(value);
    },
    []
  );

  const handleRowsPerPageChange = useCallback(
    (event) => {
      setRowsPerPage(event.target.value);
    },
    []
  );

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          Postulaciones
        </title>
      </Head>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          py: 8
        }}
      >
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Stack spacing={1}>
                <Typography variant="h4">
                  Postulaciones
                </Typography>
              </Stack>
            </Stack>
            <PostulacionesSearch />
            <PostulacionesTable
              count={data.length}
              items={postulaciones}
              onDeselectAll={postulacionesSelection.handleDeselectAll}
              onDeselectOne={postulacionesSelection.handleDeselectOne}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              onSelectAll={postulacionesSelection.handleSelectAll}
              onSelectOne={postulacionesSelection.handleSelectOne}
              page={page}
              rowsPerPage={rowsPerPage}
              selected={postulacionesSelection.selected}
            />
          </Stack>
        </Container>
      </Box>
    </AuthGuard>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
