using UnityEngine;
using UnityEngine.Events;

public class SwitchBlock : ItemBlock
{
    [SerializeField] private UnityEvent onHitted;

    public override bool Hit()
    {
        //í@Ç¢ÇΩå„ÇæÇ¡ÇΩèÍçá(Ç†ÇÈÇ¢ÇÕí@ÇØÇ»Ç©Ç¡ÇΩèÍçá)
        if (!base.Hit())
            return false;
        
        //í@ÇØÇΩèÍçá
        onHitted?.Invoke();
        return true;
    }
}
