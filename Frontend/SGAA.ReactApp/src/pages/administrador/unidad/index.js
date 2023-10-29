import { Box, Breadcrumbs, Container, Link, Stack, Typography } from '@mui/material';
import Head from 'next/head';
import { useCallback, useEffect, useMemo, useState } from 'react';
import { getUnidades } from '/src/api/administrador';
import { AuthGuard } from '/src/guards/auth-guard';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { UnidadesSearch } from '/src/sections/unidad/unidad-search';
import { UnidadesTable } from '/src/sections/unidad/unidad-table';
import { applyPagination } from '/src/utils/apply-pagination';

const useUnidades = (filteredData, page, rowsPerPage) => {
  return useMemo(
    () => {
      return applyPagination(filteredData, page, rowsPerPage);
    },
    [filteredData, page, rowsPerPage]
  );
};

const Page = () => {
  const [data, setData] = useState([]);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [searchText, setSearchText] = useState('');
  const lcSearchText = searchText.toLowerCase();
  const filteredData = data.filter((item) =>
    Object.values(item).some(
      field => field?.toString().toLowerCase().includes(lcSearchText)
    )
  );

  const unidades = useUnidades(filteredData, page, rowsPerPage);

  const handleSearchChange = useCallback(
    (event) => {
      console.log(event.target.value);
      setSearchText(event.target.value);
      setPage(0);
    },
    []
  );

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

  useEffect(() => {
    getUnidades()
      .then((response) => {
        setData(response);
      });
  }, []);

  return (
    <AuthGuard roles={['Administrador']}>
      <Head>
        <title>
          SGAA - Unidades
        </title>
      </Head>
      <Box>
        <Container maxWidth="xl">
          <Stack spacing={3}>
            <Stack
              direction="row"
              justifyContent="space-between"
              spacing={4}
            >
              <Breadcrumbs aria-label="breadcrumb">
                <Link underline="hover" color="inherit" href="/">
                  Inicio
                </Link>
                <Link
                  underline="hover"
                  color="inherit"
                  href="/administrador/unidad"
                >
                  Unidades
                </Link>
              </Breadcrumbs>
            </Stack>
          </Stack>
        </Container>
      </Box>
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
                  Unidades
                </Typography>
              </Stack>
            </Stack>
            <UnidadesSearch onSearchChange={handleSearchChange} />
            <UnidadesTable
              count={filteredData.length}
              items={unidades}
              onPageChange={handlePageChange}
              onRowsPerPageChange={handleRowsPerPageChange}
              page={page}
              rowsPerPage={rowsPerPage}
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
