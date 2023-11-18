import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import { getUnidad, registrarPublicacion } from '/src/api/propietario';
import { FancyFormPage } from '/src/components/fancy-form-page';
import { Layout as DashboardLayout } from '/src/layouts/dashboard/layout';
import { PublicacionCrearForm } from '/src/sections/publicacion/publicacion-crear-form';

const Page = () => {
  const router = useRouter();
  const unidadId = router.query.unidadId;
  const [unidad, setUnidad] = useState(null);

  useEffect(() => {
    if (router.isReady)
      getUnidad(unidadId)
        .then((response) => {
          setUnidad(response);
        });
  }, [router.isReady]);

  const onPublicacionCreated = (result) => {
    if (result)
      router.push('/propietario/publicacion');
  };

  const breadcrumbsConfig = [
    { url: '/inicio', title: 'Inicio' },
    { url: '/propietario/unidad', title: 'Unidades' },
    { url: '/propietario/unidad/' + unidadId, title: unidadId },
    { url: '/propietario/unidad/' + unidadId + '/publicacion/crear', title: 'Crear' }
  ];

  return (
    <FancyFormPage
      roles={['Propietario']}
      form={unidad && <PublicacionCrearForm unidad={unidad} handleSubmit={registrarPublicacion} handleConfirmationChange={onPublicacionCreated} />}
      title={'Crear Publicación'}
      breadcrumbsConfig={breadcrumbsConfig}>
    </FancyFormPage>
  );
};

Page.getLayout = (page) => (
  <DashboardLayout>
    {page}
  </DashboardLayout>
);

export default Page;
