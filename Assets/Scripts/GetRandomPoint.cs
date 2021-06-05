using UnityEngine;

static class GetRandomPoint
{
    public static Vector3 Get()
    {
        Vector3 position = new Vector3();
        int wallCount = 0;


            // Использование данного кода не совсем корректно, спавнил обьекты в стенах 
        //Vector3 randomDirection = Random.insideUnitSphere * 10;
        //NavMeshHit hit;
        //if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
        //{
        //    position = hit.position;
        //}     

        do
        {
            // ставим предпологаемые координаты
            position = new Vector3(Random.Range(-9.5f, 9.5f), 0.5f, Random.Range(-9.5f, 9.5f));

            Collider[] hitColliders = Physics.OverlapSphere(position, 1);

            wallCount = 0;
            foreach (Collider tempC in hitColliders)
            {
                // считаем сколько стен по этим координатам 
                if (tempC.tag == "Wall")
                    wallCount++;
            }

        } while (wallCount != 0);

        position.y = 0;
        return position;
    }
}
