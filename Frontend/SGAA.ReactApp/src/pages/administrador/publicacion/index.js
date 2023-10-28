import { Box, Container, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useMemo, useState } from 'react';
import { useSelection } from '/src/hooks/use-selection';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PublicacionesSearch } from '/src/sections/publicacion/publicacion-search';
import { PublicacionesTable } from '/src/sections/publicacion/publicacion-table';
import { applyPagination } from '/src/utils/apply-pagination';
import { AuthGuard } from '/src/guards/auth-guard';

const data = [
  {
    id: 2,
    montoAlquiler: 100000.50,
    inicioAlquiler: '2023/10/10',
    postulaciones: 10,
    status: 'Publicada',
    domicilioCompleto: 'Corrientes 1500 P1 D2'
  }
];

const usePublicaciones = (page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(data, page, rowsPerPage);
    },
    [page, rowsPerPage]
  );
};

const usePublicacionIds = (publicaciones) => {
  return useMemo(
    () => {
      return publicaciones.map((publicacion) => publicacion.id);
    },
    [publicaciones]
  );
};

const Page = () => {
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const publicaciones = usePublicaciones(page, rowsPerPage);
  const publicacionesIds = usePublicacionIds(publicaciones);
  const publicacionesSelection = useSelection(publicacionesIds);

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
          Publicaciones
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
                  Publicaciones
                </Typography>
              </Stack>
            </Stack>
            <PublicacionesSearch />
            <PublicacionesTable
              count={data.length}
              items={publicaciones}
              onDeselectAll={publicacionesSelection.handleDeselectAll}
              onDeselectOne={publicacionesSelection.handleDeselectOne}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              onSelectAll={publicacionesSelection.handleSelectAll}
              onSelectOne={publicacionesSelection.handleSelectOne}
              page={page}
              rowsPerPage={rowsPerPage}
              selected={publicacionesSelection.selected}
            />
          </Stack>
        </Container>
      </Box>
    </AuthGuard >
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
