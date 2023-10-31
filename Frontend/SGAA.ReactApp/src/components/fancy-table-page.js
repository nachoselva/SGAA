import PlusIcon from '@heroicons/react/24/solid/PlusIcon';
import { Box, Button, Container, Stack, SvgIcon, Typography } from '@mui/material';
import Head from 'next/head';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import { FancyBreadcrumbs } from './fancy-breadcrumbs';
import { FancyTable } from '/src/components/fancy-table';
import { AuthGuard } from '/src/guards/auth-guard';

export const FancyTablePage = (props) => {

  const router = useRouter();
  const [data, setData] = useState([]);
  const {
    getData,
    entityName,
    listName,
    breadcrumbsConfig,
    roles,
    onAddEntityRedirectTo,
    headerConfiguration,
    tableRowGenerator
  } = props;

  useEffect(() => {
    getData()
      .then((response) => {
        setData(response);
      });
  }, []);

  return (
    <AuthGuard roles={roles}>
      <Head>
        <title>
          SGAA - {listName}
        </title>
      </Head>
      <FancyBreadcrumbs breadcrumbsConfig={breadcrumbsConfig}>
      </FancyBreadcrumbs>
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
                  {listName}
                </Typography>
              </Stack>
              {onAddEntityRedirectTo &&
                <Button
                  startIcon={(
                    <SvgIcon fontSize="small">
                      <PlusIcon />
                    </SvgIcon>
                  )}
                  variant="contained"
                  onClick={() => router.push(onAddEntityRedirectTo)}
                  sx={{ display: { xs: 'none', sm: 'flex' } }}
                >
                  {entityName}
                </Button>
              }
              {onAddEntityRedirectTo &&
                <Button
                  variant="contained"
                  onClick={() => router.push(onAddEntityRedirectTo)}
                  sx={{ display: { xs: 'flex', sm: 'none' } }}
                >
                  <SvgIcon>
                    <PlusIcon />
                  </SvgIcon>
                </Button>
              }
            </Stack>
            <FancyTable data={data} headerConfiguration={headerConfiguration} tableRowGenerator={tableRowGenerator} />
          </Stack>
        </Container>
      </Box>
    </AuthGuard>
  );
};
